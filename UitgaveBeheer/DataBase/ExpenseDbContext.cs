using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UitgaveBeheer.Domain;

namespace UitgaveBeheer.DataBase
{
    public class ExpenseDbContext : DbContext
    {

        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options) : base (options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categorie>().HasData(new Categorie { Id = 1, Name = "Wagen" });
            modelBuilder.Entity<Categorie>().HasData(new Categorie { Id = 2, Name = "Energie" });
        }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Categorie> Categories { get; set; }
    }
}
