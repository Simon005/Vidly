﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Entity
{
    public class MyDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public MyDbContext()
        {

        }

    }
}