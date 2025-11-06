using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TRIAL.Persistence.entity
{
    public partial class Registration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PasswordHash { get; set; }

        public string? Role { get; set; }

        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        [EmailAddress(ErrorMessage = "Please enter a valid E-mail address.")]
        [Required]
        public string Email { get; set; }

        public DateOnly? DateOfBirth { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        // for reset account's password
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpiration { get; set; }
        public Subjects subjects { get; set; }

        public ICollection<Marks> subject { get; set; }

        public ICollection<HomeworkStudent> homeworkTs { get; set; }
    }

}