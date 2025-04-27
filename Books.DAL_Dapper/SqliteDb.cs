using System.Collections.Generic;
using Books.Model;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Books.DAL_Dapper;

public class SqliteDb : ICrud
{
    private readonly SqliteConnection _connection;

    public SqliteDb(string connectionString)
    {
        _connection = new SqliteConnection(connectionString);
    }
    
    public IEnumerable<Book>? GetAll()
    {
        const string sql = "SELECT * FROM table_books";
        var books = _connection.Query<Book>(sql);
        return books;
    }

    public bool Add(Book book)
    {
        var sql = """
                   INSERT INTO table_books(title, author) 
                   VALUES(@Title, @Author)
                   """;
        var result = _connection.Execute(sql, new {Title = book.Title, Author = book.Author});
        return result > 0;
    }

    public bool Update(Book book)
    {
        var sql = """
                   UPDATE table_books 
                   SET title = @Title, author = @Author 
                   WHERE id = @Id
                   """;
        var result = _connection.Execute(sql, new {Title = book.Title, Author = book.Author, Id = book.Id});
        return result > 0;
    }

    public bool Delete(int id)
    {
        var sql = """
                   DELETE FROM table_books 
                          WHERE id = @Id
                   """;
        var result = _connection.Execute(sql, new {Id = id});
        return result > 0;
    }
}