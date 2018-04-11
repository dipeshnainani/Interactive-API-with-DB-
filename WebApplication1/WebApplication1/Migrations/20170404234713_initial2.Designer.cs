using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MessengerAPI.Models;

namespace MessengerAPI.Migrations
{
    [DbContext(typeof(BooksAPIContext))]
    [Migration("20170404234713_initial2")]
    partial class initial2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("MessengerAPI.Models.Author", b =>
                {
                    b.Property<int>("AuthorID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FName");

                    b.Property<string>("LName")
                        .IsRequired();

                    b.HasKey("AuthorID");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("MessengerAPI.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("PublishedDate");

                    b.Property<string>("Title");

                    b.HasKey("BookId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("MessengerAPI.Models.BookAuthor", b =>
                {
                    b.Property<int>("BookId");

                    b.Property<int>("AuthorId");

                    b.HasKey("BookId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("BookAuthor");
                });

            modelBuilder.Entity("MessengerAPI.Models.BookAuthor", b =>
                {
                    b.HasOne("MessengerAPI.Models.Author", "Author")
                        .WithMany("BookAuthors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MessengerAPI.Models.Book", "Book")
                        .WithMany("BookAuthors")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
