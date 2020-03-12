using System;
using System.Collections.Generic;
using System.Text;
using Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SparkPayments.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
