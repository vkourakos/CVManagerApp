using System.ComponentModel.DataAnnotations.Schema;

namespace CVManagerapp.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CVId { get; set; }
        [ForeignKey("CVId")]
        public CV CV { get; set; }
    }
}