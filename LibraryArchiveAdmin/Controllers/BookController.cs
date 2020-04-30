using LibraryArchiveAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace LibraryArchiveAdmin.Controllers
{
    public class BookController : Controller
    {
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string title, string author, string genre, string year, string amount)
        {
            if (title == String.Empty || author == String.Empty || genre == String.Empty
                || year == String.Empty || amount == String.Empty)
            {
                ViewBag.ResultMessage = Resourses.strings.AllFieldsRequired;
                return View("Result");
            }
            int amount1;
            int year1;
            try
            {
                year1 = Int32.Parse(year);

            }
            catch (Exception ex)
            {

                Logger.WriteLog("LibAdminLog.txt", ex.Message);
                ViewBag.ResultMessage = Resourses.strings.YearInvalid;
                return View("Result");
            }
            try
            {
                amount1 = Int32.Parse(amount);
                if (amount1 < 1)
                {
                    ViewBag.ResultMessage = Resourses.strings.AmountMustBeGreaterThan0;
                    return View("Result");
                }
            }
            catch (Exception ex)
            {

                Logger.WriteLog("LibAdminLog.txt", ex.Message);
                ViewBag.ResultMessage = Resourses.strings.AmountInvalid;
                return View("Result");
            }

            Book book = new Book() { Amount = amount1, Title = title, Genre = genre, Year = year1, Author = author };
            BookContext db = new BookContext();

            try
            {
                
                db.Books.Add(book);
                db.SaveChanges();
                ViewBag.ResultMessage = Resourses.strings.BookAdded;

            }
            catch (Exception ex)
            {
                Logger.WriteLog("LibAdminLog.txt", ex.Message);
                ViewBag.ResultMessage = Resourses.strings.AddingError;
            }
            finally
            {
                db.Dispose();
            }

            return View("Result");
        }
        public ActionResult List()
        {
            try
            {
                BookContext db = new BookContext();
                IEnumerable<Book> books = db.Books;
                if (books != null && books.Count() > 0)
                {
                    ViewBag.Books = books;
                    return View();
                }
                ViewBag.ResultMessage = Resourses.strings.ListEmpty;
                return View("Result");
            }
            catch (Exception ex)
            {
                Logger.WriteLog("LibAdminLog.txt", ex.Message);

            }

            ViewBag.ResultMessage = Resourses.strings.BookRemovalError;
            return View("Result");

        }
        [HttpGet]
        public ActionResult Remove()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Remove(int Id)
        {
            BookContext db = new BookContext();
            try
            {
                Book bookToRemove = db.Books.Find(Id);

                if (bookToRemove != null)
                {
                    db.Books.Remove(bookToRemove);
                    db.SaveChanges();
                    @ViewBag.ResultMessage = Resourses.strings.BookRemoved;

                }
                else
                    @ViewBag.ResultMessage = Resourses.strings.BookNotFound;
            }
            catch (Exception ex)
            {
                Logger.WriteLog("LibAdminLog.txt", ex.Message);
                ViewBag.ResultMessage = Resourses.strings.BookRemovalError;
            }
            finally
            {
                db.Dispose();
            }

            return View("Result");
        }

        public ActionResult RemoveFromList(int Id)
        {
            return Remove(Id);
        }
    }
}