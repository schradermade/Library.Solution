using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
  public class AuthorsController : Controller // allows AuthorsController to operate as a Controller
  {
    private readonly LibraryContext _db; // Defining the Database as Library
    public AuthorsController(LibraryContext db) //constructor for the controller 
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Author> model = _db.Authors.ToList();
      return View(model);
    }

    public ActionResult SearchAuthor(string name)
    {
          var thisAuthor = _db.Authors //return Author name and id 
          .Include(author => author.JoinEntries) //find books(JoinEntries) related to the author
          .ThenInclude(join => join.Book) //With all join entries add the related book 
          .FirstOrDefault(author => author.AuthorName == name); // find the Author that matches the ID
      return View(thisAuthor);
    }



    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Author author)
    {
      _db.Authors.Add(author);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisAuthor = _db.Authors //return Author name and id 
          .Include(author => author.JoinEntries) //find books(JoinEntries) related to the author
          .ThenInclude(join => join.Book) //With all join entries add the related book 
          .FirstOrDefault(author => author.AuthorId == id); // find the Author that matches the ID
      return View(thisAuthor);
    }

    public ActionResult Edit(int id)
    {
      var thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id); // finds the first match and assigns it to "thisAuthor".
      return  View(thisAuthor);
    }

    [HttpPost]
    public ActionResult Edit(Author author) //author is an object that contains all properties, not just the ID
    {
      _db.Entry(author).State = EntityState.Modified; // holding the information in a bucket
      _db.SaveChanges();// pour the bucket into the database
      return RedirectToAction("Index"); //returning to index page in authors
    }

    public ActionResult Delete(int id)
    {
      var thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
      return View(thisAuthor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
      _db.Authors.Remove(thisAuthor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}