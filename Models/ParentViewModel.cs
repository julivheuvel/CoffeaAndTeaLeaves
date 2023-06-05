#pragma warning disable CS8618
namespace CoffeaAndTeaLeaves.Models;

public class ParentViewModel
{

    // ================
    // User
    // ================
        public User User { get; set; }
        public List<User> AllUsers { get; set; }


    // ================
    // Pins
    // ================
        public Pin Pin { get; set; }
        public List<Pin> AllPins { get; set; }
        

    // ================
    // Coffees
    // ================
        public Coffee Coffee { get; set; }

        // generic all coffee
        public List<Coffee> AllCoffees { get; set; }

        // only coffee chosen for order
        public List<Coffee> CoffeSelections { get; set; }

    // ================
    // Contacts
    // ================
        public Contact Contact { get; set; }
        
        // generic all contacts
        public List<Contact> AllContacts { get; set; }
        
        
    // ================
    // Orders
    // ================
        public Order Order { get; set; }

        // generic all orders - admin view
        public List<Order> AllOrders { get; set; }

        // only orders for user
        public List<Order> UsersOrder { get; set; }
        


    // ================
    // Likes
    // ================
        public Like Like { get; set; }
        public List<Like> AllLikes { get; set; }
        
    // include List<Order> ???



}