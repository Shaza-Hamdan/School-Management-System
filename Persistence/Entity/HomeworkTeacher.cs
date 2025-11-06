using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRIAL.Persistence.entity
{
    public class HomeworkTeacher
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Homework { get; set; }

        [Required]
        [Column("Discription")]
        public string Discription { get; set; }


        public DateTime Deadline { get; set; }


        [ForeignKey("subjects")]
        public int subjectsId { get; set; }
        public Subjects subjects { get; set; }

        public ICollection<HomeworkStudent> HomeworkStudent { get; set; }
    }
}