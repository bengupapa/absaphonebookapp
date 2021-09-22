using Microsoft.EntityFrameworkCore;
using phonebookserver.data.Models;
using System;

namespace phonebookserver.data
{
    public class PhoneBookDbContext: DbContext
    {
        public DbSet<PhoneBook> PhoneBooks { get; set; }
        public DbSet<PhoneBookEntry> Entries { get; set; }

        public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options) : base(options) 
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhoneBook>()
                .HasMany<PhoneBookEntry>(x => x.Entries)
                .WithOne();

            modelBuilder.Entity<PhoneBookEntry>().HasKey(e => e.Id);
        }
    }
}
