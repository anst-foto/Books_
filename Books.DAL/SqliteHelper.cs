using System.Collections.Generic;
using System.Data;
using Books.Model;
using Microsoft.Data.Sqlite;

namespace Books.DAL;

public static class SqliteHelper
{
    public static bool Exist(string sql, string connectionString)
    {
        var db = new SqliteConnection(connectionString);
        db.Open();
        
        var command = new SqliteCommand(sql, db);
        var result = command.ExecuteNonQuery();
        
        db.Close();
        
        return result > 0;
    }

    public static IEnumerable<Book>? ExistSelect(string sql, string connectionString)
    {
        var db = new SqliteConnection(connectionString);
        db.Open();
        
        var command = db.CreateCommand();
        command.CommandText = sql;
        
        var reader = command.ExecuteReader();
        if (!reader.HasRows) return null;
        
        var books = new List<Book>();
        while (reader.Read())
        {
            var book = new Book
            {
                Id = reader.GetInt32("id"),
                Title = reader.GetString("title"),
                Author = reader.GetString("author")
            };
            books.Add(book);
        }
        
        db.Close();
        
        return books;
    }
}