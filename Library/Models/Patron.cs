using System.Collections.Generic;

namespace Library.Models
{
  public class Patron
  {
    public Patron()
    {
      this.JoinEntries = new HashSet<BookPatron>();
    }

    public int PatronId { get; set; }
    public string PatronName { get; set; }

    public ICollection<BookPatron> JoinEntries { get; }
  }
}  