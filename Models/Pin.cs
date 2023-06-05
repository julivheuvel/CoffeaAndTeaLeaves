#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeaAndTeaLeaves.Models;


public class Pin
{

    // ================
    // Pin ID
    // ================
    [Key]
    public int PinId { get; set; }

    // ================
    // Content
    // ================
    [Required(ErrorMessage="content is required")]
    public string Content { get; set; }

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
    public User? Poster { get; set; }
    
    // ================
    // Liking PinPosts - ManyToMany
    // ================
    public List<Like> PinPoster { get; set; } = new List<Like>();
}
