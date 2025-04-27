using System.Collections.Generic;
using Books.Model;

namespace Books.DAL_Dapper;

public interface ICrud
{
    public IEnumerable<Book>? GetAll();
    public bool Add(Book book);
    public bool Update(Book book);
    public bool Delete(int id);
}