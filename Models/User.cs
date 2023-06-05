#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeaAndTeaLeaves.Models;

public class User
{
    // ================
    // User ID
    // ================
    [Key]
    public int UserId { get; set; }

    // ================
    // FirstName
    // ================
    [Required(ErrorMessage = "is required.")]
    [Display(Name="First Name")]
    [MaxLength(10, ErrorMessage ="must be less than 10 characters")]
    public string FirstName { get; set; }
    
    // ================
    // LastName
    // ================
    [Required(ErrorMessage = "is required.")]
    [Display(Name="Last Name")]
    [MinLength(2, ErrorMessage ="must be at least 2 characters long")]
    public string LastName { get; set; }
    
    // ================
    // Username
    // ================
    [Required(ErrorMessage = "is required.")]
    [Display(Name="Username")]
    [UniqueUsername]
    [MinLength(2, ErrorMessage ="must be at least 2 characters long")]
    public string Username { get; set; }

    // ================
    // Email
    // ================
    [Required(ErrorMessage = "is required.")]
    [UniqueEmail]
    [Display(Name="Email Address")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    // ================
    // Birthday
    // ================
    [Required(ErrorMessage ="is required")]
    [Display(Name="Birthday")]
    [FutureDate(ErrorMessage = "cannot be future date")]
    [OverEighteen(ErrorMessage ="must be at least 18")]
    [DataType(DataType.Date)]
    public DateTime Birthday {get;set;}   


    // ================
    // Admin
    // ================
    public bool Admin { get; set; } = false;
    // ================
    // PrimaryAdmin
    // ================
    public bool PrimaryAdmin { get; set; } = false;

    // ================
    // Admin Request Access
    // ================
    [Range(0,3)]
    public int AdminRequest { get; set; } = 0;

    // ================
    // Password
    // ================
    [Required(ErrorMessage = "is required.")]
    [Display(Name="Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    // ================
    // ConfirmPassword
    // ================
    [NotMapped]
    [Display(Name="Confirm Password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "passwords must match")]
    public string ConfirmPassword { get; set; }

    // ================
    // CreatedAt
    // ================
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // ================
    // UpdatedAt
    // ================
    public DateTime UpdatedAt { get; set; } = DateTime.Now;




    //! ===================================
    //! Relationships
    //! ===================================

    // ================
    // Friendship - OneToOne
    // ================
    public Friendship Friend { get; set; }

    // ================
    // Pin - OneToMany
    // ================
    public List<Pin> PinPosts { get; set; } = new List<Pin>();

    // ================
    // Contact - OneToMany
    // ================
    public List<Contact> UsersAddresses { get; set; } = new List<Contact>();
    

    // ================
    // Liking PinPosts - ManyToMany
    // ================
    public List<Like> UsersLike { get; set; } = new List<Like>();

    // ================
    // Order - OneToMany
    // ================
    public List<Order> UsersOrder { get; set; } = new List<Order>();
    


}







