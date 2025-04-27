using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Books.BLL;
using Books.DAL_Dapper;
using Books.Model;

namespace Books.Desktop;

public partial class MainWindow : Window
{
    private readonly List<Book> _books;
    private readonly IService _service;
    
    public MainWindow()
    {
        _books = [];
        
        var connectionString = File.ReadAllText("db.config");
        _service = new Service(new SqliteDb(connectionString));
        
        InitializeComponent();
        
        //ListOfBooks.ItemsSource = _books;
        LoadBooks();
    }
    
    private void ListOfBooks_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var book = (Book)ListOfBooks.SelectedItem!;
        
        InputID.Text = book.Id.ToString();
        InputTitle.Text = book.Title;
        InputAuthor.Text = book.Author;
    }

    private void ButtonSave_OnClick(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(InputID.Text))
        {
            _service.AddBook(new Book()
            {
                Title = InputTitle.Text,
                Author = InputAuthor.Text
            });
        }
        else
        {
            var bookNew = new Book()
            {
                Id = int.Parse(InputID.Text),
                Title = InputTitle.Text!,
                Author = InputAuthor.Text!
            };
            _service.UpdateBook(bookNew);
        }
        
        Clear();
        LoadBooks();
    }

    private void ButtonDelete_OnClick(object? sender, RoutedEventArgs e)
    {
        var book = (Book)ListOfBooks.SelectedItem!;
        _service.DeleteBook(book.Id);
        
        Clear();
        LoadBooks();
    }

    private void ButtonClear_OnClick(object? sender, RoutedEventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        InputID.Clear();
        InputTitle.Clear();
        InputAuthor.Clear();
    }

    private void LoadBooks()
    {
        var books = _service.GetAllBooks();
        
        if (books == null) return;
        
        _books.Clear();
        foreach (var book in books)
        {
            _books.Add(book);
        }
        
        ListOfBooks.ItemsSource = books;
    }
}