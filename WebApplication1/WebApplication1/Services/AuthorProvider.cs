using MessengerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Dapper;
using System.Data;
using Microsoft.Extensions.Configuration;
/*
 * Repository for Author
 * It defines the methods add, update, delete, getbyid and getall
 * for the author. It implements the interface IAuthorRepository.
 * 
 * It also opens the connection to the database and performs SQL queries
 * in the function.
 * 
 * Reference to connect to database: http://techbrij.com/asp-net-core-postgresql-dapper-crud
 * */

namespace MessengerAPI.Services
{
    public class AuthorRepository : IAuthorRespository
    {
        private String connectionString;

        // constructor of the class. used to get the connection
        public AuthorRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }

        // opens the connection to the database.
        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        // adds an author
        public Author Add(Author book)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Author>("INSERT INTO authors (authorid,fname,lname,bookid) VALUES(@authorid,@fname,@lname,@bookid)", book).FirstOrDefault();
            }
        }

        // deletes an entry
        public void Delete(Author book)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM authors WHERE authorid = " + book.authorid);
            }
        }

        // gets all the authors from the table
        public IEnumerable<Author> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Author>("SELECT * FROM authors");
            }
        }

        // gets the author by id
        public Author GetById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Author>("SELECT * FROM authors WHERE authorid =" +id).FirstOrDefault();
            }
        }

        // update values in the database
        public void Update(Author book)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE authors SET authorid = @authorid, fname = @fname, bookid = @bookid, lname = @lname  WHERE authorid = " + book.authorid);
            }
        }
    }
}
