using System.Collections.Generic;
using Books.Model;
using Microsoft.Data.Sqlite;

namespace Books.DAL;

public class SqliteDb : ICrud
{
    private readonly string _connectionString;
    private SqliteConnection _db;

    public SqliteDb(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public IEnumerable<Book>? GetAll()
    {
        const string sql = "SELECT * FROM table_books";
        var books = SqliteHelper.ExistSelect(sql, _connectionString);
        return books;
    }

    public bool Add(Book book)
    {
        var sql = $"""
                   INSERT INTO table_books(title, author) 
                   VALUES('{book.Title}', '{book.Author}')
                   """;
        var result = SqliteHelper.Exist(sql, _connectionString);
        return result;
    }

    public bool Update(Book book)
    {
        var sql = $"""
                   UPDATE table_books 
                   SET title = '{book.Title}', author = '{book.Author}' 
                   WHERE id = {book.Id}
                   """;
        var result = SqliteHelper.Exist(sql, _connectionString);
        return result;
    }

    public bool Delete(int id)
    {
        var sql = $"""
                   DELETE FROM table_books 
                          WHERE id = {id}
                   """;
        var result = SqliteHelper.Exist(sql, _connectionString);
        return result;
    }
}