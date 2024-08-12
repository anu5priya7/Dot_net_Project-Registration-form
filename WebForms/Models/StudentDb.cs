using System.ComponentModel.DataAnnotations.Schema;

namespace WebForms.Models
{
    public class StudentDb
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public DateTime Dob { get; set; }
        public List<string> Qualifications { get; set; }
        public string Gender { get; set; }
        public List<string> SelectedQualifications { get; set; }
        public List<string> Hobbies { get; set; }
    }
}
