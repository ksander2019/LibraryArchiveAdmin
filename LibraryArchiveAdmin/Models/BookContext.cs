﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryArchiveAdmin.Models
{
    public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

    }
}