#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeaAndTeaLeaves.Models;

public class Contact
{

    // ================
    // Contact
    // ================
    [Key]
    public int ContactId { get; set; }

    // ================
    // FirstName
    // ================
    [Required(ErrorMessage = "is required")]
    [Display(Name="First Name")]
    public String FirstName { get; set; }

    // ================
    // LastName
    // ================
    [Required(ErrorMessage = "is required")]
    [Display(Name="Last Name")]
    public String LastName { get; set; }

    // ================
    // Email
    // ================
    [EmailAddress]
    [UniqueEmail]
    [Required(ErrorMessage = "is required")]
    [Display(Name="Email Address")]
    public String Email { get; set; }

    // ================
    // Address
    // ================
    [Required(ErrorMessage = "is required")]
    public String Address { get; set; }

    // ================
    // SecondaryAddress
    // ================
    [Display(Name="Secondary Address")]
    public String SecondaryAddress { get; set; }  = "";

    // ================
    // City
    // ================
    [Required(ErrorMessage = "is required")]
    public String City { get; set; }

    // ================
    // Country
    // ================
    [Required(ErrorMessage = "is required")]
    public String Country { get; set; }
    
    // ================
    // PostalCode
    // ================
    [Required(ErrorMessage = "is required")]
    public String PostalCode { get; set; }
    

    // ================
    // User - OneToMany
    // ================
    public int UserId { get; set; }
    public User? Addresses { get; set; }


}