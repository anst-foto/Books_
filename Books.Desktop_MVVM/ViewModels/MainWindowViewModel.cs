using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;
using Books.BLL;
using Books.DAL;
using Books.Model;
using ReactiveUI;

namespace Books.Desktop_MVVM.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private int? _id;
    public int? Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }

    private string? _title;
    public string? Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }
    
    private string? _author;
    public string? Author
    {
        get => _author;
        set => this.RaiseAndSetIfChanged(ref _author, value);
    }
    
    public ObservableCollection<Book> Books { get; set; } = [];

    private Book? _selectedBook;
    public Book? SelectedBook
    {
        get => _selectedBook;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedBook, value);
            
            Id = value?.Id;
            Title = value?.Title;
            Author = value?.Author;
        } 
    }
    
    private readonly IService _service;
    
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; }
    public ReactiveCommand<Unit, Unit> ClearCommand { get; }

    public MainWindowViewModel()
    {
        var connectionString = File.ReadAllText("db.config");
        _service = new Service(new SqliteDb(connectionString));
        
        LoadBooks();

        SaveCommand = ReactiveCommand.Create(() =>
        {
            if (Id.HasValue)
            {
                var book = new Book()
                {
                    Id = _id.Value,
                    Title = _title,
                    Author = _author
                };
                _service.UpdateBook(book);
            }
            else
            {
                var bookNew = new Book()
                {
                    Title = _title,
                    Author = _author
                };
                _service.AddBook(bookNew);
            }

            Clear();
            LoadBooks();
        });

        DeleteCommand = ReactiveCommand.Create(() =>
        {
            if (!Id.HasValue) return;
            
            _service.DeleteBook(Id.Value);
                
            Clear();
            LoadBooks();
        });
        
        ClearCommand = ReactiveCommand.Create(Clear);
    }

    private void Clear()
    {
        SelectedBook = null;
        
        Id = null;
        Title = null;
        Author = null;
    }
    
    private void LoadBooks()
    {
        var books = _service.GetAllBooks();
        
        if (books == null) return;
        
        Books.Clear();
        foreach (var book in books)
        {
            Books.Add(book);
        }
    }
}