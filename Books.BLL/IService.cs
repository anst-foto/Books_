using System.Collections.Generic;
using Books.Model;

namespace Books.BLL;

public interface IService
{
    public IEnumerable<Book>? GetAllBooks();
    
    public IEnumerable<Book>? GetBooksByTitle(string title);
    public IEnumerable<Book>? GetBooksByAuthor(string author);
    
    public bool AddBook(Book book);
    public bool UpdateBook(Book book);
    public bool DeleteBook(int id);
}