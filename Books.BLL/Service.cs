using System.Collections.Generic;
using System.Linq;
using Books.DAL;
using Books.Model;

namespace Books.BLL;

public class Service : IService
{
    private readonly ICrud _db;

    public Service(ICrud db)
    {
        _db = db;
    }
    
    public IEnumerable<Book>? GetAllBooks()
    {
        return _db.GetAll();
    }

    public IEnumerable<Book>? GetBooksByTitle(string title)
    {
        var books = _db.GetAll();
        return books?.Where(b => b.Title.ToLower().Contains(title.ToLower()));
    }

    public IEnumerable<Book>? GetBooksByAuthor(string author)
    {
        var books = _db.GetAll();
        return books?.Where(b => b.Author.ToLower().Contains(author.ToLower()));
    }

    public bool AddBook(Book book)
    {
        return _db.Add(book);
    }

    public bool UpdateBook(Book book)
    {
        return _db.Update(book);
    }

    public bool DeleteBook(int id)
    {
        return _db.Delete(id);
    }
}