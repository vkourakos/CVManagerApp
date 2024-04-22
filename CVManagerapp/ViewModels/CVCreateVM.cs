using System.ComponentModel.DataAnnotations;

namespace CVManagerapp.ViewModels
{
    public class CVCreateVM
    {
        public string UserId { get; set; }
        [StringLength(100)]
        [Display(Name = "Job Title")]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [MinimumAge(18, ErrorMessage = "You must be at least 18 years old.")]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [RegularExpression(@"^69\d{8}$", ErrorMessage = "Phone number must start with '69' and be 10 digits long")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }


        
    }
    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateOfBirth)
            {
                if (dateOfBirth.AddYears(_minimumAge) > DateTime.Today)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
