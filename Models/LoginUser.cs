#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CoffeaAndTeaLeaves.Models;

[NotMapped]
public class LoginUser
{

    // ================
    // Username
    // ================
    [Required(ErrorMessage = "Username is required")]
    [Display(Name = "Username")]
    public string LoginUsername { get; set; }


    // ================
    // Password
    // ================

    [Required(ErrorMessage = "Password is required")]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string LoginPassword { get; set; }
    
}