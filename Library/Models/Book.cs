using System.Collections.Generic;

namespace Library.Models
{
  public class Book
  {
    public Book()
    {
      this.JoinEntries = new HashSet<BookAuthor>();
    }

    public int BookId { get; set; }
    public string BookName { get; set; }
    public virtual ApplicationUser User { get; set; }

    public ICollection<BookAuthor> JoinEntries { get; }
  }
}