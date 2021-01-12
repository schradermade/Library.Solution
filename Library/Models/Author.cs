using System.Collections.Generic;

namespace Library.Models
{
  public class Author
  {
    public Author()
    {
      this.JoinEntries = new HashSet<BookAuthor>();
    }

    public int AuthorId { get; set; }
    public string AuthorName { get; set; }

    public ICollection<BookAuthor> JoinEntries { get; }
  }
}  