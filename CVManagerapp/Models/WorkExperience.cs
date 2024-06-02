using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVManagerapp.Models
{
    public class WorkExperience
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        [Column(TypeName = "Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public int CVId { get; set; }
        [ForeignKey("CVId")]
        public CV CV { get; set; }
    }
}