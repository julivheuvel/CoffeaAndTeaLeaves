using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeaAndTeaLeaves.Models;

namespace CoffeaAndTeaLeaves.Controllers;

public class CoffeeController : Controller
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

    private CandTContext db;
    public CoffeeController(CandTContext context)
    {
        db = context;
    }


    // ================
    // View Coffee Dashboard
    // ================
    [HttpGet("/candt/coffee")]
    public IActionResult All()
    {   
        User? user = db.Users
            .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UUID"));

        ParentViewModel AllModels = new ParentViewModel
        {   
            AllCoffees = db.Coffees
                .OrderByDescending(c => c.CreatedAt)
                .ToList(),
            User = user
        };

        return View("AllCoffee", AllModels);
    }


    // ================
    // Coffee Admin View
    // ================
    [HttpGet("/candt/coffee/admin")]
    public IActionResult AdminAllCoffee()
    {
        User? user = db.Users
            .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UUID"));
        
        if(user.Admin){
            Console.WriteLine((int)uid);
        }


        ParentViewModel AllModels = new ParentViewModel
        {   
            AllCoffees = db.Coffees
                .OrderByDescending(c => c.UpdatedAt)
                .ToList(),
            User = user
        };

        return View("AdmincoffeeView", AllModels);
    }



    // ================
    // Create A Coffee
    // ================
    [SessionCheck]
    [HttpPost("/candt/coffee/new")]
    public IActionResult NewCoffee(Coffee newCoffee)
    {   
        User? user = db.Users
            .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UUID"));
        
        if(user.Admin){
            Console.WriteLine((int)uid);
        }


        ParentViewModel AllModels = new ParentViewModel
        {   
            AllCoffees = db.Coffees
                .OrderByDescending(c => c.CreatedAt)
                .ToList(),
            User = user
        };

        if(!ModelState.IsValid)
        {
            return AdminAllCoffee();
        }

        db.Coffees.Add(newCoffee);
        db.SaveChanges();
        
        return RedirectToAction("AdminAllCoffee", AllModels);
    }

    // ================
    // View A Coffee
    // ================
    [HttpGet("/candt/coffee/{oneCoffeeId}/view")]
    public IActionResult GetOne(int oneCoffeeId)
    {
        // ? makes return able to be nullable
        Coffee? oneCoffee = db.Coffees
            // .Include(p =>p.DishLikes)
            //     .ThenInclude(l => l.User)
            .FirstOrDefault(coffee => coffee.CoffeeId == oneCoffeeId);

        User? user = db.Users
            .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UUID"));

        ParentViewModel AllModels = new ParentViewModel
        {   
            AllCoffees = db.Coffees
                .OrderByDescending(c => c.CreatedAt)
                .ToList(),
            User = user,
            Coffee = oneCoffee
        };


        // if oneCoffee null, redirect
        if(oneCoffee == null)
        {
            return RedirectToAction("All", AllModels);
        }

        return View("OneCoffee", AllModels);
    }



    // ================
    // Edit A Coffee - Render
    // ================
    [HttpGet("/candt/coffee/{oneCoffeeId}/edit")]
    public IActionResult EditCoffee(int oneCoffeeId)
    {
        // ? makes return able to be nullable
        Coffee? oneCoffee = db.Coffees
            // .Include(p =>p.DishLikes)
            //     .ThenInclude(l => l.User)
            .FirstOrDefault(coffee => coffee.CoffeeId == oneCoffeeId);

        User? user = db.Users
            .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UUID"));

        ParentViewModel AllModels = new ParentViewModel
        {   
            AllCoffees = db.Coffees
                .OrderByDescending(c => c.CreatedAt)
                .ToList(),
            User = user
        };


        // if oneCoffee null, redirect
        if(oneCoffee == null)
        {
            return RedirectToAction("All", AllModels);
        }

        return View("EditCoffee", oneCoffee);
    }


    // ================
    // Edit A Coffee - Post
    // ================
    [HttpPost("/candt/coffee/{editCoffeeId}/update")]
    public IActionResult UpdateCoffee(Coffee editedCoffee, int editCoffeeId)
    {   
        User? user = db.Users
            .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UUID"));
        
        if(user.Admin){
            Console.WriteLine((int)uid);
        }

        if(!ModelState.IsValid)
        {
            return AdminAllCoffee();
        }
        
            Coffee? dbCoffee = db.Coffees.FirstOrDefault(coffee => coffee.CoffeeId == editCoffeeId);
            if(dbCoffee == null)
            {   
                Console.WriteLine("========");
                return RedirectToAction("AdminAllCoffee");
            }

            dbCoffee.Name = editedCoffee.Name;
            dbCoffee.Price = editedCoffee.Price;
            dbCoffee.Region = editedCoffee.Region;
            dbCoffee.Roast = editedCoffee.Roast;
            dbCoffee.ImgUrl = editedCoffee.ImgUrl;
            dbCoffee.Quantity = editedCoffee.Quantity;
            
            
            // if it exists && validated, update info
            dbCoffee.UpdatedAt = DateTime.Now;

            db.Coffees.Update(dbCoffee);
            db.SaveChanges();
            Console.WriteLine("====***********************=====");
            return RedirectToAction("GetOne", new {oneCoffeeId= editCoffeeId});


    }


    // ============================================
    // Delete Coffee Route
    // ============================================
    [HttpPost("/candt/{deletedCoffeeId}/coffee")]
    public IActionResult DeleteCoffee(int deletedCoffeeId)
    {
        
        Coffee? coffee = db.Coffees.FirstOrDefault(c => c.CoffeeId == deletedCoffeeId);

        if(coffee != null)
        {
            db.Coffees.Remove(coffee);
            db.SaveChanges();
        }
        return RedirectToAction("All");
    }
}