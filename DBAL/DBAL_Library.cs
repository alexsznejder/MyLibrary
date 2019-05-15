using MyLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyLibrary.DBAL
{
    public class DBAL_Library : DbContext
    {
        public DBAL_Library(): base("MyDB")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Book>().ToTable("Books");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> usersDb { get; set; }
        public DbSet<Book> booksDb { get; set; }

        public List<User> GetUserList()
        {
            return usersDb.ToList();
        }

        public List<Book> GetBookList()
        {
            return booksDb.ToList();
        }

        public void CreateUser(User user)
        {
            usersDb.Add(user);
            SaveChanges();
        }

        public void CreateBook(Book book)
        {
            booksDb.Add(book);
            SaveChanges();
        }

        public void EditBook(Book book)
        {
            Entry(book).State = System.Data.Entity.EntityState.Modified;
            SaveChanges();
        }

        public void DeleteBook(Book book)
        {
            booksDb.Attach(book);
            booksDb.Remove(book);
            SaveChanges();
        }
    }
}