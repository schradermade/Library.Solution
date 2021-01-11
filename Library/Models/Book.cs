using System.Collections.Generic;

namespace Library.Models
{
  public class Book
  {
    public Book()
    {
      this.JoinEntries = new HashSet<BookPatron>();
    }

    public int BookId { get; set; }
    public string BookName { get; set; }

    public ICollection<BookPatron> JoinEntries { get; }
  }
}  