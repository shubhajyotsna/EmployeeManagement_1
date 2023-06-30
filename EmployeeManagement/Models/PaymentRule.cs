using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    public class PaymentRule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentRuleId { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string PaymentRuleName { get; set; }
    }
}
