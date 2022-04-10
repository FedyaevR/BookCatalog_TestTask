using BookCatalog.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookCatalog.ViewModels
{
    public class BookListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        List<Author> _authorName { get; set; }
        Location _location { get; set; }
        List<Genre> _genreName { get; set; }
        ObservableCollection<Book> _books { get; set; }
        string _searchQuery { get; set; }
        bool _editIsEnbaled { get; set; } = false;

        Book _selectedBook;
        #region Property
        public bool EditIsEnabled
        {
            get { return _editIsEnbaled; }
            set
            {
                if (_editIsEnbaled != value)
                {
                    _editIsEnbaled = value;
                    OnPropertyChanged("EditIsEnabled");
                }
            }
        }
        public string SearchQuery
        {
            get { return _searchQuery; }
            set
            {
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    OnPropertyChanged("SearchQuery");
                }
            }
        }
        public ObservableCollection<Book> BookList
        {
            get
            {
                return _books;
            }
            set
            {
                if (_books != value)
                {
                   
                    _books = value;
                    OnPropertyChanged("BookList");
                }
            }

        }
       public List<Author> AuthorsList
        {
            get { return _authorName; }
            set
            {
                if (_authorName != value)
                {
                    _authorName = value;
                    OnPropertyChanged("AuthorsList");
                }
            }
        }
        public List<Genre> GenresList
        {
            get { return _genreName; }
            set
            {
                if (_genreName != value)
                {
                    _genreName = value;
                    OnPropertyChanged("GenresList");
                }   
            }
        }
        public int LocationRow
        {
            get { return _location.Row; }
            set
            {
                if (_location.Row != value)
                {
                    _location.Row = value;
                    OnPropertyChanged("LocationRow");
                }
            }
        }
        public int LocationRack
        {
            get { return _location.Rack; }
            set
            {
                if (_location.Rack != value)
                {
                    _location.Rack = value;
                    OnPropertyChanged("LocationRack");
                }
            }
        }
        public int LocationShelf
        {
            get { return _location.Shelf; }
            set
            {
                if (_location.Shelf != value)
                {
                    _location.Shelf = value;
                    OnPropertyChanged("LocationShelf");
                }
            }
        }
        #endregion
        public ICommand SearchCommand { get; protected set; }
        public ICommand EditCommand { get; protected set; }
        public INavigation Navigation { get; set; }
        public BookListViewModel()
        {
            _books = new ObservableCollection<Book>();
            _location = new Location();
            var list =  App.BookRepository.ShowAllBook();
            for (int i = 0; i < list.Count; i++)
            {
                _books.Add(new Book() { BookName = list[i].BookName, Authors = list[i].Authors, Genres = list[i].Genres, Location = list[i].Location}) ;
            }
            SearchCommand = new Command(Search);
        }

   
        private async void Search()
        {
            var list = await App.BookRepository.SearchBookAsync(_searchQuery);
            _books.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                _books.Add(new Book() { BookName = list[i].BookName, Authors = list[i].Authors, Genres = list[i].Genres, Location = list[i].Location });
            }
            OnPropertyChanged("BookList");
        }

        public Book SelectedBook
        {
            get { return _selectedBook; }
            set
            {
                if (_selectedBook != value)
                {
                    _selectedBook = value;
                    AuthorsList = _selectedBook.Authors;
                    GenresList = _selectedBook.Genres;
                    LocationRow = _selectedBook.Location.Row;
                    LocationRack = _selectedBook.Location.Rack;
                    LocationShelf = _selectedBook.Location.Shelf;
                    EditIsEnabled = true;
                    OnPropertyChanged("SelectedBook");
                }
            }
        }
      
        public void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

    }
}
