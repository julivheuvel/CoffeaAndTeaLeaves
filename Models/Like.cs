#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace CoffeaAndTeaLeaves.Models;

public class Like
{
    [Key]
    public int LikeId { get; set; }
    // ================
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    // ================

    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    // ================




    //! ===================================
    //! Relationships
    //! ===================================
    public int UserId { get; set; }
    public User? User { get; set; }
    public int PinId { get; set; }
    public Pin? Pin { get; set; }
    
}