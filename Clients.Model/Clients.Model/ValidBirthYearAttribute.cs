using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Clients.Model
{
    public class ValidBirthYearAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext
        validationContext)
        {
            if (value is not int) return new ValidationResult("Invalid birth year format.The birth year must be an integer.");
            

            int age = DateTime.Now.Year - (int)value;
            if (age > 120)
                return new ValidationResult($"Age ({age}) can not be more than 120 years.");
            if (age < 0)
                return new ValidationResult($"Age ({age}) can not be negative.");
            return ValidationResult.Success;
        }
    }
}
