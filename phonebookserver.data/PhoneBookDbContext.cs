using Microsoft.EntityFrameworkCore;
using phonebookserver.data.Models;
using System;

namespace phonebookserver.data
{
    public class PhoneBookDbContext: DbContext
    {
        public DbSet<PhoneBook> PhoneBooks { get; set; }
        public DbSet<PhoneBookEntry> PhoneBookEntries { get; set; }

        public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options) : base(options) 
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhoneBook>().HasKey(x => x.Id);
            //modelBuilder.Entity<PhoneBook>()
            //    .HasMany(x => x.Entries)
            //    .WithOne(x => x.PhoneBook)
            //    .HasForeignKey(x => x.PhoneBookId);

            modelBuilder.Entity<PhoneBookEntry>().HasKey(x => x.Id);
            modelBuilder.Entity<PhoneBookEntry>()
                .HasOne(x => x.PhoneBook)
                .WithMany(x => x.Entries)
                .HasForeignKey(x => x.PhoneBookId);
        }
    }
}