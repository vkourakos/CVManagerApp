namespace CVManagerapp.Models
{
    public class Certification
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? IssueDate { get; set; }
        public string IssuingOrganization { get; set; }
    }
}