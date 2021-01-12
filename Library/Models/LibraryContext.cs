using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//Identifying the Database Schema

namespace Library.Models
{
  public class LibraryContext : IdentityDbContext<ApplicationUser>
  {
    public virtual DbSet<Author> Authors { get; set; } //DBSets are new tables being created. 
    public DbSet<Book> Books { get; set; }

    public DbSet<BookAuthor> BookAuthor { get; set; }

    public LibraryContext(DbContextOptions options) : base(options) { } 
  }
}