using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CoffeaAndTeaLeaves.Models;




// ! ==================================================
// ! CUSTOM VALIDATIONS
// ! ==================================================

// ===============
//  DATE CANNOT BE SET FOR FUTURE
// ===============
public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if(((DateTime)value) >= DateTime.Now.Date){
            return new ValidationResult("Date entered must before today's date");
        }else{
            return ValidationResult.Success;
        }
        // You first may want to unbox "value" here and cast to to a DateTime variable!
    }
}


// ===============
//  MUST BE OVER 18
// ===============
public class OverEighteenAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        int year = DateTime.Now.Year - ((DateTime)value).Year;
        if((((DateTime)value).Month > DateTime.Now.Month) || (((DateTime)value).Month == DateTime.Now.Month && ((DateTime)value).Day > DateTime.Now.Day)){
            year--;
        }
        if(year < 18){
            return new ValidationResult("Must be older than 18");
        }else{
            return ValidationResult.Success;
        }
        // You first may want to unbox "value" here and cast to to a DateTime variable!
    }
}


// ===============
//  MUST BE UNIQUE EMAIL
// ===============
public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            // if value is empty
            return new ValidationResult("Email is required!");
        }

        // This will connect us to our database since we are not in our Controller
        CandTContext db = (CandTContext)validationContext.GetService(typeof(CandTContext));
        // Check to see if there are any records of this email in our database
        if (db.Users.Any(e => e.Email == value.ToString()))
        {
            // If yes, throw an error
            return new ValidationResult("Email is already in use!");
        }
        else
        {
            // If no, proceed
            return ValidationResult.Success;
        }
    }
}
// ===============
//  MUST BE UNIQUE Username
// ===============
public class UniqueUsernameAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            // if value is empty
            return new ValidationResult("Username is required!");
        }

        // This will connect us to our database since we are not in our Controller
        CandTContext db = (CandTContext)validationContext.GetService(typeof(CandTContext));
        // Check to see if there are any records of this Username in our database
        if (db.Users.Any(e => e.Username == value.ToString()))
        {
            // If yes, throw an error
            return new ValidationResult("Username is already in use!");
        }
        else
        {
            // If no, proceed
            return ValidationResult.Success;
        }
    }
}


// ===============
//  MUST USE LETTERS ONLY
// ===============
public class LettersOnlyAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        string stringValue = value.ToString();

        if (!Regex.IsMatch(stringValue, @"^[a-zA-Z\s]+$"))
        {
            return new ValidationResult("Only letters and spaces are allowed.");
        }

        return ValidationResult.Success;
    }
}


// ===============
//  MUST USE LETTERS/NUMBERS ONLY
// ===============
public class LettersAndNumbersAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        string stringValue = value.ToString();

        if (!Regex.IsMatch(stringValue, @"^[a-zA-Z0-9]+$"))
        {
            return new ValidationResult("Only letters and numbers are allowed.");
        }

        return ValidationResult.Success;
    }
}