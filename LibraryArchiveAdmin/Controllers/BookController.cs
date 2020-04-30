﻿using LibraryArchiveAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            int amount1;
            int year1;

            amount1 = Int32.Parse(amount);
            year1 = Int32.Parse(year);

            if (amount1 < 1)
            {
                ViewBag.ResultMessage = "Кількість примірників не може бути меншою за 1.";
                return View("Result");
            }

            Book book = new Book() { Amount = amount1, Title = title, Genre = genre, Year = year1, Author = author };
            ViewBag.ResultMessage = "Книгу додано.";
            return View("Result");
        }
        public ActionResult List()
        {
            IEnumerable<Book> books = null;
            if (books != null)
            {
                ViewBag.Books = books;
                return View();
            }
            ViewBag.ResultMessage = "Список книг порожній";
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
            Book bookToRemove = null;

            if (bookToRemove != null)
            {
                @ViewBag.ResultMessage = "Книгу видалено.";
            }
            else
                @ViewBag.ResultMessage = "Книгу не знайдено.";

            return View("Result");
        }

        public ActionResult RemoveFromList(int Id)
        {
            return Remove(Id);
        }
    }
}