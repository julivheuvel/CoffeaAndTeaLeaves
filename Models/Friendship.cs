#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeaAndTeaLeaves.Models;


public class Friendship
{

    // ================
    // Friendship ID
    // ================
    [Key]
    public int FriendshipId { get; set; }




    //! ===================================
    //! Relationships
    //! ===================================

    // ================
    // User - OneToOne
    // ================
    public int UserId { get; set; }
    public User User { get; set; }

}