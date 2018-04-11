using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessengerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using Npgsql;
using Dapper;

using System.Data;
using Microsoft.Extensions.Configuration;

/*
 * Repository for Book
 * It defines the methods add, update, delete, getbyid and getall
 * for the books. It implements the interface IBookRepository.
 * 
 * It also opens the connection to the database and performs SQL queries
 * in the function.
 * 
 * Reference to connect to database: http://techbrij.com/asp-net-core-postgresql-dapper-crud
 * */
namespace MessengerAPI.Services
{
    public static class Extensions
    {
        public static long Insert<T>(this IDbConnection connection, T entityToInsert, IDbTransaction transaction = null, int? commandTimeout = default(int?)) where T : class
        {
            var tableName = (entityToInsert.GetType().Name + "s").ToLower();
            // doesn't work in .net Core
            //var props = typeof(T).getProperties();
            var r = connection.Query("select column_name, data_type, character_maximum_length from INFORMATION_SCHEMA.COLUMNS where table_name = '" + tableName +"'");
            foreach(var col in r)
            {
                if (col.column_name == null) { }
            }
            return 0;
        }

    }
    public class BookRepository : IBookRepository
    {
        private string connectionString;

        // constructor which helps in setting up the connection to the database
        public BookRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }

        // sets up the connection
        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        // adds a book
        public Book Add(Book book)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Book>("INSERT INTO books (bookid,publisheddate,title) VALUES(@bookid,@publisheddate,@title)", book).FirstOrDefault();
            }
        }

        // deletes a book
        public void Delete(Book book)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM books WHERE bookid = " + book.bookid);
            }
        }

        // gets all books
        public IEnumerable<Book> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Book>("SELECT * FROM books");
            }

        }

        // gets book by id
        public Book GetById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Book>("SELECT * FROM books WHERE bookid = "  + id).FirstOrDefault();
            }
        }

        // updates a value of the book 
        public void Update(Book book)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE books SET bookid = @bookid, publisheddate = @publisheddate, title = @title WHERE bookid = " + book.bookid);
            }
        }
    }
}
