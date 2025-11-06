using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRIAL.Persistence.entity
{
    public class HomeworkStudent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string FilePath { get; set; }


        [Column(TypeName = "DATETIME")]
        public DateTime Created { get; set; } // = DateTime.Now;

        [ForeignKey("Registration")]
        public int RegistrationId { get; set; }
        public Registration Registration { get; set; }

        [ForeignKey("HomeworkT")]
        public int HomeworkTId { get; set; }
        public HomeworkTeacher HomeworkT { get; set; }
    }
}
