using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

/*
 * Model class for all the entities
 * IT has different classes for the entities and columns provided
 * in each of the tables of the entities. It basically gets the value 
 * and sets them.
 *        
 * */

namespace MessengerAPI.Models
{
    // model for author
    public class Author
    {
        public int authorid { get; set; }
        public string fname { get; set; }
        [Required]
        public string lname { get; set; }
        public IEnumerable<BookAuthor> BookAuthors { get; set; }

    }

    // model for book
    public class Book
    {
        [Key]
        public int bookid { get; set; }
        public string title { get; set; }

        public List<Author> Authors { get; set; }
        public DateTime publisheddate { get; set; }
    }
    
    // model for library
    public class Library
    {
        public string name { get; set; }
        public string bookname { get; set; }
        public int bookid { get; set; }
        public string fname { get; set; }
        [Required]
        public string lname { get; set; }
    }

    // model for patron
    public class Patron
    {
        public int patronid { get; set; }
        public string fname { get; set; }
        [Required]
        public string lname { get; set; }
        public int bookid { get; set; }
        public string bookname { get; set; }
    }

    // rename this to Book for Entity Framework
    //public class EFBook
    //{
      //  [Key]
       // public int BookId { get; set; }
        //public string Title { get; set; }

        //public IEnumerable<BookAuthor> BookAuthors { get; set; }
        //public DateTime PublishedDate { get; set; }
    //}

    // model for bookauthor
    public class BookAuthor
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }

   

    public class BooksAPIContext : DbContext
    { 
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Uncomment for Entity Framework
            /*
            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookId);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);
                */

        }
        public BooksAPIContext(DbContextOptions options) : base(options)
        {
        }
     


    }
}
