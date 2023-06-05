using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeaAndTeaLeaves.Models;

namespace CoffeaAndTeaLeaves.Controllers;

public class ContactController : Controller
{

    // storing Get UserID in session into variable uid
    private int? uid
    {
        get{
            return HttpContext.Session.GetInt32("UUID");
        }
    }

    // variable to check if logged in
    private bool loggedIn
    {
        get
        {
            return uid != null;
        }
    }


    // dependency injection => allowing us to manipulate database
    private CandTContext db;
    public ContactController(CandTContext context)
    {
        db = context;
    }

    



    // ===================
    // SHOW ALL CONTACT INFO
    // ===================
    [HttpGet("/candt/contact/all")]
    public IActionResult AllContacts()
    {   
        if(loggedIn) {

            User? user = db.Users
            .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UUID"));
            Console.WriteLine(user.FirstName);
            Console.WriteLine("!!!!!!!!!!!!!!!");
            
            
            ParentViewModel AllModels = new ParentViewModel
            {
                AllUsers = db.Users.ToList(),
                AllContacts = db.Contacts.ToList(),
                User = user,
            };
            return View("ViewContactInfo");
        }

        return RedirectToAction("Welcome", "User");
    }


    // ===================
    // NEW CONTACT INFO
    // ===================
    [SessionCheck]
    [HttpGet("/candt/contact/new")]
    public IActionResult NewContact()
    {      
        
        if(loggedIn)
        {   
            
            User? user = db.Users
            .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UUID"));
            // Console.WriteLine("here?");
            
            ParentViewModel AllModels = new ParentViewModel
            {
                AllUsers = db.Users.ToList(),
                AllContacts = db.Contacts.ToList(),
                User = user,
            };
            return View("NewContact");

        }
        return RedirectToAction("AllContacts", "Contact");

    }


    // ===================
    // CREATE CONTACT INFO
    // ===================
    [SessionCheck]
    [HttpPost("/candt/contact/create")]
    public IActionResult CreateContact(Contact newContact)
    {   
        if(!ModelState.IsValid) {
            User? user = db.Users
                .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UUID"));
            
            Console.WriteLine("================");
            
            Console.WriteLine(newContact.FirstName);
            Console.WriteLine(newContact.LastName);
            Console.WriteLine(newContact.Email);
            Console.WriteLine(newContact.Address);
            Console.WriteLine(newContact.SecondaryAddress);
            Console.WriteLine(newContact.City);
            Console.WriteLine(newContact.Country);
            Console.WriteLine(newContact.PostalCode);
            
            Console.WriteLine("*******************");
            

            ParentViewModel AllModels = new ParentViewModel
            {   
                AllContacts = db.Contacts.ToList(),
                User = user
            };
            
            Console.WriteLine("Not Valid form!");
            
            return RedirectToAction("NewContact", AllModels);
        }

        newContact.UserId = (int)uid;
        db.Contacts.Add(newContact);
        db.SaveChanges();
        Console.WriteLine("Success!!!");
        
        return RedirectToAction("AllContacts");
    }
}

