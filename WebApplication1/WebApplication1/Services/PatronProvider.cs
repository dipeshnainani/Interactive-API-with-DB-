using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessengerAPI.Models;
using Npgsql;
using Dapper;

using System.Data;
using Microsoft.Extensions.Configuration;

/*
 * Repository for Patron
 * It defines the methods add, update, delete, getbyid and getall
 * for the author. It implements the interface IPatronRepository.
 * 
 * It also opens the connection to the database and performs SQL queries
 * in the function.
 * 
 * Reference to connect to database: http://techbrij.com/asp-net-core-postgresql-dapper-crud
 * */
namespace MessengerAPI.Services
{
    public class PatronRepository : IPatronRepository
    {
        
        private string connectionString;

        // constructor to setup the connection to database
        public PatronRepository(IConfiguration configuration)
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

        // adds a patron to the database
        public Patron Add(Patron book)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Patron>("INSERT INTO patron (patronid,fname,lname,bookid,bookname) VALUES(@patronid,@fname,@lname,@bookid,@bookname)", book).FirstOrDefault();
            }
        }

        // deletes a patron
        public void Delete(Patron book)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM patron WHERE patronid = " + book.bookid );
                dbConnection.Execute("DELETE FROM authors WHERE authorid = " + book.bookid );
            }
        }

        // gets all the patrons
        public IEnumerable<Patron> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Patron>("SELECT * FROM patron");
            }
        }

        // gets patron by id
        public Patron GetById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Patron>("SELECT * FROM patron WHERE patronid = " + id).FirstOrDefault();
            }
        }

        // updates the values in the patron table
        public void Update(Patron book)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE patron SET patronid = @patronid, fname = @fname, lname = @lname, bookid=@bookid, bookname=@bookname WHERE patronid = " + book.bookid, book);
            }
        }
    }
}
