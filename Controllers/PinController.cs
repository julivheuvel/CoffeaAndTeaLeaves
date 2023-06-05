using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeaAndTeaLeaves.Models;

namespace CoffeaAndTeaLeaves.Controllers;

public class PinController : Controller
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
    public PinController(CandTContext context)
    {
        db = context;
    }


    // ================
    // Dashboard
    // ================
    [SessionCheck]
    [HttpGet("/candt/dashboard")]
    public IActionResult Dashboard(string searchString)
    {
        User? user = db.Users
            .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UUID"));
        

        
        ParentViewModel AllModels = new ParentViewModel
        {
            AllPins = db.Pins
                .OrderByDescending(p => p.CreatedAt)
                .Include(p => p.PinPoster)
                .ToList(),
            AllLikes = db.Likes.ToList(),
            AllUsers = db.Users.ToList(),
            User = user,
        };
        return View("Dashboard", AllModels);
    }

    

    // ================
    // Create A Pin
    // ================
    [SessionCheck]
    [HttpPost("/candt/pins/new")]
    public IActionResult NewPin(Pin newPin)
    {
        if(!ModelState.IsValid)
        {   
            User? user = db.Users
            .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UUID"));
        

            
            ParentViewModel AllModels = new ParentViewModel
            {
                AllPins = db.Pins
                    .OrderByDescending(p => p.CreatedAt)
                    .Include(p => p.PinPoster)
                    .ToList(),
                AllLikes = db.Likes.ToList(),
                AllUsers = db.Users.ToList(),
                User = user,
            };
            return View("Dashboard", AllModels);
        }

        newPin.UserId = (int)uid;

        db.Pins.Add(newPin);
        db.SaveChanges();
        
        return RedirectToAction("Dashboard");
    }



    // ================
    // Delete A Pin
    // ================
    [HttpPost("/candt/pins/{deletePinId}/delete")]
    public IActionResult DeletePin(int deletePinId)
    {
        Pin? pin = db.Pins.FirstOrDefault(p => p.PinId == deletePinId);
        if(pin != null)
        {
            db.Pins.Remove(pin);
            db.SaveChanges();
            Console.WriteLine("Removed pin from database");
        }

        return RedirectToAction("Dashboard");
    }
    

}