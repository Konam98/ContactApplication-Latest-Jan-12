using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ContactApplication.Models;
using ContactApplication.Entities;

namespace ContactApplication.Contexts
{
    public class ApplicationDbContext : DbContext
    {
      
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);
        // }
         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       {
       // optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ContactApplication2;Username=postgres;Password=Maycg1805$");
       }

        public virtual DbSet<Contact> Contacts { get; set; }   
        public virtual DbSet<User> User {get;set;}
        

    }
}