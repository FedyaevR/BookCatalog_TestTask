using BookCatalog.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookCatalog.ViewModels
{
    public class BookAddViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Book Book;
        private string _bookName;
        private List<string> _genreList;
        private string _genre;
        public List<string> Genres;
        private string _authorName;
        public List<string> AuthorNames;
        private List<int> _rowList;
        private List<int> _rackList;
        private List<int> _shelfList;
        private bool _addIsEnabled = false;
        Location _location;
        public INavigation Navigation { get; set; }
        public ICommand AddBookCommand { get; protected set; }
        public ICommand AddAuthorCommand { get; protected set; }
        public ICommand AddGenreCommand { get; protected set; }
        #region Property
        public bool AddButtonEnabled
        {
            get{ return _addIsEnabled; }
            set
            {
                if (_addIsEnabled != value)
                {
                   
                    _addIsEnabled = value;
                    OnPropertyChanged("AddButtonEnabled");
                    
                }
            }
        }

        public List<string> GenreList
        {
            get 
            {
                return _genreList; 
            }
            set
            {
                if (_genreList != value)
                {
                    _genreList = value;
                    OnPropertyChanged("GenreList");
                }
            }
           
        }
        public List<int> RowList
        {
            get
            {
                return _rowList;
            }
            set
            {
                if (_rowList != value)
                {
                    _rowList = value;
                    OnPropertyChanged("RowList");
                }
            }

        }
        public List<int> RackList
        {
            get
            {
                return _rackList;
            }
            set
            {
                if (_rackList != value)
                {
                    _rackList = value;
                    OnPropertyChanged("RackList");
                }
            }

        }
        public List<int> ShelfList
        {
            get
            {
                return _shelfList;
            }
            set
            {
                if (_shelfList != value)
                {
                    _shelfList = value;
                    OnPropertyChanged("ShelfList");
                }
            }

        }
        public string BookName
        {
            get { return _bookName; }
            set
            {
                if (_bookName != value)
                {
                    _bookName = value;
                    OnPropertyChanged("BookName");
                }
            }
        }
        public string AuthorName
        {
            get { return _authorName; }
            set
            {
                _authorName = value;
            }
        }

        public int SelectedRow
        {
            get { return _location.Row; }
            set
            {
                if (_location.Row != value)
                {
                    _location.Row = value;
                    OnPropertyChanged("SelectedRow");
                }
            }
        }
        public int SelectedRack
        {
            get { return _location.Rack; }
            set
            {
                if (_location.Rack != value)
                {
                    _location.Rack = value;
                    OnPropertyChanged("SelectedRack");
                }
            }
        }
        public int SelectedShelf
        {
            get { return _location.Shelf; }
            set
            {
                if (_location.Shelf != value)
                {
                    _location.Shelf = value;
                    OnPropertyChanged("SelectedShelf");
                }
            }
        }
        public string Genre
        {
            get { return _genre; }
            set
            {
                if (_genre != value)
                {
                    _genre = value;
                    OnPropertyChanged("Genre");
                }
            }

        }
        #endregion
        public BookAddViewModel()
        {
            Book = new Book();
            Genres = new List<string>();
            _location = new Location();
            AuthorNames = new List<string>();
            _genreList = new List<string>()
            {
               "Horror","Detective","Adventure","Novel"

            };
            RowList = new List<int>()
            {
                1,2,3,4,5,6,7,8
            };
            RackList = new List<int>()
            {
                1,2,3,4,5,6,7,8,9,10
            };
            ShelfList = new List<int>()
            {
                1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20
            };
          
            AddBookCommand = new Command(AddBook);
            AddAuthorCommand = new Command(AddAuthor);
            AddGenreCommand = new Command(AddGenre);
        }
      
        private void AddGenre(object obj)
        {
            if (_genre != null)
            {
                Genres.Add(_genre);
                if (AuthorNames != null && AuthorNames.Count > 0)
                {
                    AddButtonEnabled = true;
                }
                OnPropertyChanged("AddButtonEnabled");
            }
        }

        private void AddAuthor()
        {
            if (_authorName.Length > 3)
            {
                AuthorNames.Add(_authorName);
                if(Genres != null && Genres.Count > 0)
                {
                    AddButtonEnabled = true;
                }
                OnPropertyChanged("AddButtonEnabled");
            }
           
        }
        private async void AddBook()
        {
           
            if (_bookName.Length> 0 && GenreList.Count > 0 && AuthorNames.Count > 0 && _location != null)
            {
                await App.BookRepository.AddNewBookAsync(_bookName, Genres, AuthorNames, _location);
                await Navigation.PopAsync();
                
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
