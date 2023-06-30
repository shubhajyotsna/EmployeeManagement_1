using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        public string EmployeeName { get; set; }

        [ForeignKey("Designation")]
        public int DesignationId { get; set; } // Foreign key property

        public virtual Designation? Designation { get; set; } // Navigation property

        [ForeignKey("WorkHour")]
        public int WorkHourId { get; set; } // Foreign key property

        public virtual WorkHours? WorkHour { get; set; } // Navigation property

        [ForeignKey("PaymentRule")]
        public int PaymentRuleId { get; set; } // Foreign key property

        public virtual PaymentRule? PaymentRule { get; set; } // Navigation property
    }

}
