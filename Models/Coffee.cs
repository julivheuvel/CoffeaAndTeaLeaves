#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeaAndTeaLeaves.Models;

public class Coffee
{
    // Access Modifiers = public or private keywords
    // ================
    // CoffeeId
    // ================
    [Key]
    public int CoffeeId { get; set; }
    
    // ================
    // Name
    // ================
    [Required(ErrorMessage =" is required")]
    public string Name { get; set; }

    // ================
    // Price
    // ================
    [Required(ErrorMessage =" is required")]
    public double Price { get; set; }
    
    // ================
    // IMG URL
    // ================
    [Required(ErrorMessage =" is required")]
    public string ImgUrl { get; set; }
    

    // ================
    // Region
    // ================
    [Required(ErrorMessage =" is required")]
    public string Region { get; set; }
    

    // ================
    // Roast
    // ================
    [Required(ErrorMessage =" is required")]
    public string Roast { get; set; }

    // ================
    // Quantity
    // ================
    [Required(ErrorMessage =" is required")]
    public int Quantity { get; set; }
    

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
    // Coffee to Order - OneToMany
    // ================
    // public int OrderId { get; set; }
    // public Order? MainOrder { get; set; }
    



}