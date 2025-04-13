using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Books.BLL;
using Books.DAL;
using Books.Model;

namespace Books.Desktop;

public partial class MainWindow : Window
{
    private List<Book> _books;
    private readonly IService _service;
    
    public MainWindow()
    {
        var connectionString = File.ReadAllText("db.config");
        _service = new Service(new SqliteDb(connectionString));
        
        InitializeComponent();
        
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
            var book = (Book)ListOfBooks.SelectedItem!;
            _service.UpdateBook(book);
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
        
        ListOfBooks.ItemsSource = null;
        
        var books = _service.GetAllBooks();
        ListOfBooks.ItemsSource = books;
    }
}