using MyLibrary.Models;
using MyLibrary.DBAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyLibrary.ViewModels;
using MyLibrary.DAL;

namespace MyLibrary.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            SignUpVM user = new SignUpVM();
            if (Session["username"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(user);
            }
        }

        [HttpPost]
        public ActionResult SignUp(SignUpVM user)
        {
            DAL_Library dal_Library = new DAL_Library();
            User normUser = new User(user);
            
            List<User> users = dal_Library.GetUserList();
            if(user.username == null || user.password == null || user.confirmedpassword == null)
            {
                return View("SignUp", user);
            }
            else if (users.Any(u => u.username == user.username))
            {
                ViewBag.TheSame = "This username is already taken.";
                return View("SignUp", user);
            }
            else if (user.password != user.confirmedpassword)
            {
                ViewBag.Password = "Passwords aren't the same.";
                return View("SignUp", user);
            }
            dal_Library.CreateUser(normUser);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            LogInVM user = new LogInVM();
            return View(user);
        }

        [HttpPost]
        public ActionResult LogIn(LogInVM user, string ReturnUrl)
        {
            DAL_Library dal_Library = new DAL_Library();
            var thisUser = dal_Library.GetUserList().Where(u => u.username == user.username && u.password == user.password).FirstOrDefault();
            if (thisUser == null)
            {
                ViewBag.Login = "Wrong username or password.";
                return View("LogIn", user);
            }
            else
            {
                Session["userId"] = thisUser.userId;
                Session["username"] = thisUser.username;

                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult BookList()
        {
            if(Session["userId"] != null)
            {
                BookListVM books = new BookListVM();
                DAL_Library dal_Library = new DAL_Library();
                books.books = dal_Library.GetBookList().Where(b => b.userId == int.Parse(Session["userId"].ToString())).ToList();
                return View(books);
            }
            else
            {
                return RedirectToAction("LogIn", "Home");
            }
        }

        public ActionResult AddToFavorites(int id)
        {
            Book book = new Book();
            BookListVM books = new BookListVM();
            DAL_Library dal_Library = new DAL_Library();
            books.books = dal_Library.GetBookList();
            book = books.books.Where(b => b.bookId == id).Single();
            book.like = true;
            dal_Library.EditBook(book);
            return RedirectToAction("MyFavorite", "Home");
        }

        public ActionResult RemoveFromFavorites(int id)
        {
            Book book = new Book();
            BookListVM books = new BookListVM();
            DAL_Library dal_Library = new DAL_Library();
            books.books = dal_Library.GetBookList();
            book = books.books.Where(b => b.bookId == id).Single();
            book.like = false;
            dal_Library.EditBook(book);
            return RedirectToAction("MyFavorite", "Home");
        }

        public ActionResult MyFavorite()
        {
            if (Session["userId"] != null)
            {
                MyFavoritesVM favoriteBooks = new MyFavoritesVM();
                BookListVM bookList = new BookListVM();
                DAL_Library dal_Library = new DAL_Library();
                bookList.books = dal_Library.GetBookList().Where(b => b.like == true && b.userId == int.Parse(Session["userId"].ToString())).ToList();
                favoriteBooks.books = new List<Book>();
                favoriteBooks.books = bookList.books.ToList();
                return View(favoriteBooks);
            }
            else
            {
                return RedirectToAction("LogIn", "Home");
            }
        }

        public ActionResult AddBook()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("LogIn", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult AddBook(Book book)
        {
            DAL_Library dal_Library = new DAL_Library();
            book.userId = int.Parse(Session["userId"].ToString());
            if (ModelState.IsValid)
            {
                dal_Library.CreateBook(book);
                return RedirectToAction("BookList");
            }
            return View();
        }

        public ActionResult EditBook(int id)
        {
            if (Session["userId"] != null)
            {
                DAL_Library dal_Library = new DAL_Library();
                List<Book> books = dal_Library.GetBookList();
                Book book = books.Where(b => b.bookId == id).Single();
                EditBookVM editBookVM = new EditBookVM();
                editBookVM.book = new Book();
                editBookVM.book = book;

                return View(editBookVM);
            }
            else
            {
                return RedirectToAction("LogIn", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditBook(Book book)
        {
            DAL_Library dal_Library = new DAL_Library();
            dal_Library.EditBook(book);
            return RedirectToAction("BookList");
        }

        public ActionResult DeleteBook(int id)
        {
            DAL_Library dal_Library = new DAL_Library();
            List<Book> books = dal_Library.GetBookList();
            Book book = books.Where(b => b.bookId == id).Single();
            dal_Library.DeleteBook(book);
            return RedirectToAction("BookList");
        }
    }
}