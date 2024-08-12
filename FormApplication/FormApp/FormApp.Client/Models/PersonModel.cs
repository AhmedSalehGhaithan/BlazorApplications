using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FormApp.Client.Models;

public class PersonModel
{
    //note :To add minlenght for first and last name is not logic ,but to get just the idea

    [Required(ErrorMessage = "The first name is Required")]
    [MinLength(5,ErrorMessage ="The first name is too short")]
    public string FirstName { get; set; }


    [Required]
    [MinLength(8, ErrorMessage = "The Last name is too short")]
    public string LastName { get; set; }

    public string LifeStory { get; set; }

    public bool IsInRoled { get; set; } = true;
   
    public DateOnly Birthdayte { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    [Range(1,5)]
    public int MyNumber { get; set; }

    public string Department { get; set; }

    public _EmployeeType EmployeeType { get; set; }
}

public enum _EmployeeType
{
    salary,
    Hourly,
    Commission
}