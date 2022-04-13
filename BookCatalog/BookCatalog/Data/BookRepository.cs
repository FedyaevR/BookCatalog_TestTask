using BookCatalog.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookCatalog.Data
{
    public class BookRepository
    {
        SQLiteConnection db;
        public BookRepository(string dbPath)
        {
            db = new SQLiteConnection(dbPath);
            db.CreateTable<Book>();
            db.CreateTable<Genre>();
            db.CreateTable<Author>();
            db.CreateTable<Location>();
            
        }
        /// <summary>
        /// Add new book in DB
        /// </summary>
        /// <param name="bookName"></param>
        /// <param name="genres"></param>
        /// <param name="authors"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public async Task<bool> AddNewBookAsync(string bookName, List<string> genres, List<string> authors, Location location)
        {
            int result = 0;
            if (string.IsNullOrWhiteSpace(bookName) == true)
            {
                throw new Exception("Book name can't to be empty");
            }
            //Create book
            var book = new Book() { BookName = bookName};
            result = await Task.Run(()=> db.Insert(book));
            //Add genreList
            List<Genre> genre = new List<Genre>();
            for (int i = 0; i < genres.Count; i++)
            {
                genre.Add(new Genre()
                {
                    GenreName = genres[i]
                });
                await Task.Run(() => db.Insert(genre[i]));
            }
           
            book.Genres = genre;
            //Add authorList
            List<Author> author = new List<Author>();
            for (int i = 0; i < authors.Count; i++)
            {
                author.Add(new Author()
                {
                    AuthorName = authors[i]
                });
                await Task.Run(() => db.Insert(author[i]));
            }
            book.Authors = author;

            await Task.Run(() => db.Insert(location));
            book.Location = location;
            await Task.Run(() => db.UpdateWithChildren(book));

            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public  List<Book> ShowAllBook()
        {
            if (db.Table<Book>().ToList().Count > 0)
            {
                return db.GetAllWithChildren<Book>();
            }
            return new List<Book>();
        }
        #region Old method for search book
        /// <summary>
        /// Primitive search
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <returns>List of books that matched in value</returns>
        //public async Task<List<Book>> SearchBookAsync(string searchQuery)
        //{
        //    if (db.Table<Book>().ToList().Count > 0)
        //    {
        //        var list = await Task.Run(() => db.GetAllWithChildren<Book>());
        //        var listBookName = await Task.Run(()=> list.FindAll(i => i.BookName == searchQuery));
        //        var listGenre = await Task.Run(() => list.FindAll(i =>i.Genres.Contains(i.Genres.Find(j => j.GenreName == searchQuery))));
        //        var listAuthor = await Task.Run(() => list.FindAll(i => i.Authors.Contains(i.Authors.Find(j => j.AuthorName == searchQuery))));
        //        if (listBookName.Count > 0)
        //        {
        //            return listBookName;
        //        }
        //        else if(listGenre.Count > 0)
        //        {
        //            return listGenre;
        //        }
        //        else if(listAuthor.Count > 0)
        //        {
        //            return listAuthor;
        //        }
        //        else
        //        {
        //            return db.GetAllWithChildren<Book>();
        //        }

        //    }
        //    return new List<Book>();
        //}
        #endregion

        public async Task<List<Book>> SearchBookAsync(string searchQuery)
        {
            List<Book> finalResult = new List<Book>();
            List<Book> interimResult = new List<Book>();
            if (string.IsNullOrWhiteSpace(searchQuery) == false)
            {
                interimResult = await Task.Run(() => db.Query<Book>("select* from [Book] inner join [Author] on Book.Id = Author.BookId where [AuthorName] = ? union select* from [Book] inner join [Genre] on Book.Id = Genre.BookId where [GenreName] = ?", searchQuery));
                if (interimResult.Count > 0)
                {
                    finalResult = await Task.Run(() => db.GetAllWithChildren<Book>().FindAll(i => interimResult.FindAll(j => j.BookName == i.BookName).Count > 0));
                }
                else
                {
                    finalResult = await Task.Run(() => db.GetAllWithChildren<Book>().FindAll(i => i.BookName == searchQuery));
                }

            }
            if(finalResult.Count <= 0)
            {
                finalResult = await Task.Run(() => db.GetAllWithChildren<Book>());
            }
            if(finalResult.Count > 0)
            {
                return finalResult;
            }
            return new List<Book>();
        }

    }
}
