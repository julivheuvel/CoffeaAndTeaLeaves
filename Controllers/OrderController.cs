using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeaAndTeaLeaves.Models;

namespace CoffeaAndTeaLeaves.Controllers;

public class OrderController : Controller
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
    public OrderController(CandTContext context)
    {
        db = context;
    }

    

}