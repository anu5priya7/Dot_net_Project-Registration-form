using System.ComponentModel.DataAnnotations;

namespace WebForms.Models
{
    public class ErrorViewModel
    {
        [Key] 
        public int Id { get; set; }
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
