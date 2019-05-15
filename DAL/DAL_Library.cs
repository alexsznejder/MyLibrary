using MyLibrary.DBAL;
using MyLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLibrary.DAL
{
    public class DAL_Library
    {
        public List<Book> GetBookList()
        {
            DBAL_Library dbal_Library = new DBAL_Library();
            return dbal_Library.GetBookList();
        }

        public List<User> GetUserList()
        {
            DBAL_Library dbal_Library = new DBAL_Library();
            return dbal_Library.GetUserList();
        }

        public void CreateBook(Book book)
        {
            DBAL_Library dbal_Library = new DBAL_Library();
            dbal_Library.CreateBook(book);
        }

        public void CreateUser(User user)
        {
            DBAL_Library dbal_Library = new DBAL_Library();
            dbal_Library.CreateUser(user);
        }

        public void EditBook(Book book)
        {
            DBAL_Library dbal_Library = new DBAL_Library();
            dbal_Library.EditBook(book);
        }

        public void DeleteBook(Book book)
        {
            DBAL_Library dbal_Library = new DBAL_Library();
            dbal_Library.DeleteBook(book);
        }
    }
}