using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVManagerapp.Models
{
    public class Certification
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime IssueDate { get; set; }
        public string IssuingOrganization { get; set; }
        public int CVId { get; set; }
        [ForeignKey("CVId")]
        public CV CV { get; set; }
    }
}