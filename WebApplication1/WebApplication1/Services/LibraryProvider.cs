using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessengerAPI.Models;
using Dapper;
using Npgsql;
using Microsoft.Extensions.Configuration;
using System.Data;

/*
 * Repository for Library
 * It defines the methods add, update, delete, getbyid and getall
 * for the author. It implements the interface ILibraryRepository.
 * 
 * It also opens the connection to the database and performs SQL queries
 * in the function.
 * 
 * Reference to connect to database: http://techbrij.com/asp-net-core-postgresql-dapper-crud
 * */
namespace MessengerAPI.Services
{
    public class LibraryRepository : ILibraryRepository
    {

        private String connectionString;

        // constructor to setup the connection to database
        public LibraryRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }

        // gets the connection
        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        // adds a library to the database table
        public Library Add(Library book)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Library>("INSERT INTO library (name,bookname,bookid,fname,lname) VALUES(@name,@bookname,@bookid,@fname,@lname)", book).FirstOrDefault();
            }
        }

        // deletes a library
        public void Delete(Library book)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM library WHERE name = " + book.bookname);
                dbConnection.Execute("DELETE FROM books WHERE title = " + book.bookname);
            }
        }

        // gets all the libraries
        public IEnumerable<Library> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Library>("SELECT * FROM library");
            }
        }

        // gets libraries by id
        public Library GetById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Library>("SELECT * FROM library WHERE bookid = " + id).FirstOrDefault();
            }
        }

        // updates the values in the table
        public void Update(Library book)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE library SET name = @name, bookname = @bookname ,bookid = @bookid, fname = @fname, lname = @lname  WHERE bookid = " + book.bookid,book);
            }
        }
    }
}
