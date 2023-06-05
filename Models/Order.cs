#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeaAndTeaLeaves.Models;


public class Order
{

    // ================
    // Order ID
    // ================
    [Key]
    public int OrderId { get; set; }

    // ================
    // Submitted
    // ================
    public bool Submitted { get; set; } = false;

    // ================
    // OrderReceived
    // ================
    public bool OrderReceived { get; set; } = false;

    // ================
    // Shipped
    // ================
    public bool Shipped { get; set; } = false;

    // ================
    // Delivered
    // ================
    public bool Delivered { get; set; } = false;

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
    // User - OneToMany
    // ================
    public int UserId { get; set; }
    public User? Orderer { get; set; }

    // ================
    // ContactInfo - OneToMany
    // ================


    // ================
    // PaymentMethod - OneToMany
    // ================

    // ================
    // Coffee - OneToMany
    // ================
    public List<Coffee> CoffeSelections { get; set; } = new List<Coffee>();
    
    

}