using Microsoft.EntityFrameworkCore;
//Identifying the Database Schema

namespace Library.Models
{
  public class LibraryContext : DbContext
  {
    public virtual DbSet<Patron> Patrons { get; set; } //DBSets are new tables being created. 
    public DbSet<Book> Books { get; set; }

    public DbSet<BookPatron> BookPatron { get; set; }

    public LibraryContext(DbContextOptions options) : base(options) { } 
  }
}