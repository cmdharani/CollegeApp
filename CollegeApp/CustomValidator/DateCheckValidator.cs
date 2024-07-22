﻿using System.ComponentModel.DataAnnotations;

namespace CollegeApp.CustomValidator
{
    public class DateCheckValidator:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var datetime=(DateTime?)value;

            if (datetime.HasValue && datetime < DateTime.Now)
            {
                return new ValidationResult("date should be greater than todays date");
                
            }

            return ValidationResult.Success;
        }
    }
}
