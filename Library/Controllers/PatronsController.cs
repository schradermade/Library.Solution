using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
  public class PatronsController : Controller // allows PatronsController to operate as a Controller
  {
    private readonly LibraryContext _db; // Defining the Database as Library
    public PatronsController(LibraryContext db) //constructor for the controller 
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Patron> model = _db.Patrons.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Patron patron)
    {
      _db.Patrons.Add(patron);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisPatron = _db.Patrons //return Patron name and id 
          .Include(patron => patron.JoinEntries) //find books(JoinEntries) related to the patron
          .ThenInclude(join => join.Book) //With all join entries add the related book 
          .FirstOrDefault(patron => patron.PatronId == id); // find the Patron that matches the ID
      return View(thisPatron);
    }

    public ActionResult Edit(int id)
    {
      var thisPatron = _db.Patrons.FirstOrDefault(patron => patron.PatronId == id); // finds the first match and assigns it to "thisPatron".
      return  View(thisPatron);
    }

    [HttpPost]
    public ActionResult Edit(Patron patron) //patron is an object that contains all properties, not just the ID
    {
      _db.Entry(patron).State = EntityState.Modified; // holding the information in a bucket
      _db.SaveChanges();// pour the bucket into the database
      return RedirectToAction("Index"); //returning to index page in patrons
    }

    public ActionResult Delete(int id)
    {
      var thisPatron = _db.Patrons.FirstOrDefault(patron => patron.PatronId == id);
      return View(thisPatron);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisPatron = _db.Patrons.FirstOrDefault(patron => patron.PatronId == id);
      _db.Patrons.Remove(thisPatron);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}