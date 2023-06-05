using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeaAndTeaLeaves.Models;

namespace CoffeaAndTeaLeaves.Controllers;

public class UserController : Controller
{
    private int? uid
    {
        get{
            return HttpContext.Session.GetInt32("UUID");
        }
    }
    private bool loggedIn
    {
        get
        {
            return uid != null;
        }
    }
    private CandTContext db;
    public UserController(CandTContext context)
    {
        db = context;
    }
    // ================
    // Welcome
    // ================
    [HttpGet("/candt")]
    public IActionResult Welcome()
    {
        return View("Welcome");
    }

    // ================
    // Register - Render
    // ================
    [HttpGet("/candt/user/register")]
    public IActionResult Register()
    {
        return View("Register");
    }

    // ================
    // Register - Post
    // ================
    [HttpPost("/candt/user/registeruser")]
    public IActionResult RegisterUser(User newUser)
    {
        // if(ModelState.IsValid == false)
        // {   
        //     Console.WriteLine("TESTTTTTTTTT");
            
        //     return Register();
        // }
        
        Console.WriteLine("================");
        
        // hashing password
        PasswordHasher<User> hashBrowns = new PasswordHasher<User>();
        newUser.Password = hashBrowns.HashPassword(newUser, newUser.Password);

        db.Users.Add(newUser);
        db.SaveChanges();

        HttpContext.Session.SetInt32("UUID", newUser.UserId);
        HttpContext.Session.SetString("Name", newUser.FirstName + " " + newUser.LastName);
        HttpContext.Session.SetString("Username", newUser.Username);
        return RedirectToAction("Dashboard", "Pin");

    }

    // ================
    // Login - Render
    // ================
    [HttpGet("/candt/user/login")]
    public IActionResult Login()
    {
        return View("Login");
    }

    // ===================
    // LOGIN - Post
    // ===================
    [HttpPost("/candt/user/loginuser")]
    public IActionResult LoginUser(LoginUser loginUser)
    {
        
        if(ModelState.IsValid == false)
        {
            return Login();
        }

        User? dbUser = db.Users.FirstOrDefault(u => u.Username == loginUser.LoginUsername);

        if(dbUser == null)
        {
            ModelState.AddModelError("LoginUser", "Not found");
            return Login();
        }

        PasswordHasher<LoginUser> hashBorwns = new PasswordHasher<LoginUser>();
        PasswordVerificationResult pwCompare = hashBorwns.VerifyHashedPassword(loginUser, dbUser.Password, loginUser.LoginPassword);

        // if passwords don't match
        if(pwCompare == 0)
        {
            ModelState.AddModelError("LoginPassword", "invalid password");
            return Login();
        }

        // no issues
        HttpContext.Session.SetInt32("UUID", dbUser.UserId);
        HttpContext.Session.SetString("Name", dbUser.FirstName + " " + dbUser.LastName);
        HttpContext.Session.SetString("Username", dbUser.Username);
        return RedirectToAction("Dashboard", "Pin");
    }


    // ===================
    // LOGOUT
    // ===================
    [HttpPost("/candt/user/logout")]
    public IActionResult Logout()
    {

        HttpContext.Session.Clear();

        return RedirectToAction("Index", "Home");
    }

    // ===================
    // Request Admin Access
    // ===================
    [HttpPost("/candt/user/{adminUserId}/requestAdmin")]
    public IActionResult RequestAdminAccess(int adminUserId)
    {
        User? adminUser = db.Users.FirstOrDefault(u => u.UserId == adminUserId);

        adminUser.AdminRequest = 1;
        adminUser.UpdatedAt = DateTime.Now;
        db.Users.Update(adminUser);
        db.SaveChanges();

        return RedirectToAction("Dashboard", "Pin");
    }

    // ===================
    // Remove Admin Access
    // ===================
    [HttpPost("/candt/user/{adminUserId}/removeAdmin")]
    public IActionResult RemoveAdminAccess(int adminUserId)
    {
        User? adminUser = db.Users.FirstOrDefault(u => u.UserId == adminUserId);
        
        // setting a remove request
        adminUser.AdminRequest = 2;
        adminUser.UpdatedAt = DateTime.Now;
        db.Users.Update(adminUser);
        db.SaveChanges();

        return RedirectToAction("Dashboard", "Pin");
    }
    // ===================
    // Ignore Admin Request
    // ===================
    [HttpPost("/candt/user/{ignoreUserId}/ignoreAdmin")]
    public IActionResult IgnoreAdminRequest(int ignoreUserId)
    {
        User? adminUser = db.Users.FirstOrDefault(u => u.UserId == ignoreUserId);
        
        // setting request back to 0
        adminUser.AdminRequest = 0;
        // adminUser.UpdatedAt = DateTime.Now;
        db.Users.Update(adminUser);
        // Console.WriteLine("testtttt");
        
        db.SaveChanges();

        return RedirectToAction("Dashboard", "Pin");
    }

    // ===================
    // SetAdmin
    // ===================
    [HttpPost("/candt/user/{adminUserId}/setAdmin")]
    public IActionResult SetAdmin(int adminUserId)
    {
        User? adminUser = db.Users.FirstOrDefault(u => u.UserId == adminUserId);
        Console.WriteLine(adminUser.Admin);

        adminUser.Admin = true;

        // clearing request
        adminUser.AdminRequest = 0;
        adminUser.UpdatedAt = DateTime.Now;
        db.Users.Update(adminUser);
        db.SaveChanges();
        Console.WriteLine("saved!");
        


        return RedirectToAction("Dashboard", "Pin");
        
    }

    // ===================
    // Remove Admin
    // ===================
    [HttpPost("/candt/user/{adminUserId}/deleteAdmin")]
    public IActionResult DeleteAdmin(int adminUserId)
    {
        User? adminUser = db.Users.FirstOrDefault(u => u.UserId == adminUserId);
        Console.WriteLine(adminUser.Admin);

        adminUser.Admin = false;

        // clearing request
        adminUser.AdminRequest = 0;
        adminUser.UpdatedAt = DateTime.Now;
        db.Users.Update(adminUser);
        db.SaveChanges();

        return RedirectToAction("Dashboard", "Pin");
        
    }


    // ================
    // Delete User
    // ================
    [HttpPost("/candt/user/{deleteUserId}/delete")]
    public IActionResult DeleteUser(int deleteUserId)
    {
        User? user = db.Users.FirstOrDefault(p => p.UserId == deleteUserId);

        // safeguarding admin
        if(user.UserId == 1){
            return RedirectToAction("Dashboard", "Pin");
        }

        // otherwise user may be deleted
        if(user != null)
        {
            db.Users.Remove(user);
            db.SaveChanges();
            Console.WriteLine("Removed User from database");
        }

        return RedirectToAction("Dashboard", "Pin");
    }


    // ================
    // AdminMgmt
    // ================
    [HttpGet("/candt/user/admin")]
    public IActionResult AdminMgmt()
    {
        User? user = db.Users
            .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UUID"));
        

        
        ParentViewModel AllModels = new ParentViewModel
        {
            AllUsers = db.Users.ToList(),
            User = user,
        };
        return View("AdminMgmt", AllModels);
    }


    // ===================
    // Set PrimaryAdmin
    // ===================
    [HttpPost("/candt/user/{primaryAdminUserId}/setPrimaryAdmin")]
    public IActionResult SetPrimaryAdmin(int primaryAdminUserId)
    {
        User? primaryAdminUser = db.Users.FirstOrDefault(u => u.UserId == primaryAdminUserId);
        Console.WriteLine(primaryAdminUser.Admin);

        primaryAdminUser.PrimaryAdmin = true;
        primaryAdminUser.UpdatedAt = DateTime.Now;
        db.Users.Update(primaryAdminUser);
        db.SaveChanges();
        Console.WriteLine("saved!");


        return RedirectToAction("AdminMgmt", "User");
        
    }

    // ===================
    // Remove PrimaryAdmin
    // ===================
    [HttpPost("/candt/user/{primaryAdminUserId}/deletePrimaryAdmin")]
    public IActionResult DeletePrimaryAdmin(int primaryAdminUserId)
    {
        User? primaryAdminUser = db.Users.FirstOrDefault(u => u.UserId == primaryAdminUserId);
        Console.WriteLine(primaryAdminUser.Admin);

        primaryAdminUser.PrimaryAdmin = false;
        primaryAdminUser.UpdatedAt = DateTime.Now;
        db.Users.Update(primaryAdminUser);
        db.SaveChanges();

        return RedirectToAction("AdminMgmt", "User");
        
    }

}



