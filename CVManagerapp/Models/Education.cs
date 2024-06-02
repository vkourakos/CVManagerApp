using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVManagerapp.Models
{
    public class Education
    {
        public int Id { get; set; }
        public string Institution { get; set; }
        public string Degree { get; set; }
        public string FieldOfStudy { get; set; }
        [Column(TypeName = "Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public int CVId { get; set; }
        [ForeignKey("CVId")]
        public CV CV{ get; set; }
    }
}
