using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BookCatalog.Models
{
    [Table("Book")]
    public class Book
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        [OneToOne]
        public Location Location { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Author> Authors { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Genre> Genres { get; set; }
        public string BookName { get; set; }
    }
    [Table("Location")]
    public class Location
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        [ForeignKey(typeof(Book))]
        public int BookId { get; set; }
        public int Row { get; set; }
        public int Rack { get; set; }
        public int Shelf { get; set; }
        [OneToOne]
        public Book Book { get; set; }
    }
    [Table("Author")]
    public class Author
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string AuthorName { get; set; }
        [ForeignKey(typeof(Book))]
        public int BookId { get; set; }
        [ManyToOne]
        public Book Book { get; set; }
    }
    [Table("Genre")]
    public class Genre 
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        [ForeignKey(typeof(Book))]
        public int BookId { get; set; }
        public string GenreName { get; set; }
        [ManyToOne]
        public Book Book { get; set; }
    
    }

}
