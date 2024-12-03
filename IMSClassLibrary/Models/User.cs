using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSClassLibrary.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }


        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        [StringLength(20)]
        public string Nrc { get; set; }
        public string Address { get; set; }

        public string ImagePath { get; set; }

        [DefaultValue("active")]
        [StringLength(20)]
        public string? Status { get; set; }

        [StringLength(50)]
        public string? CreatedBy { get; set; }

        [StringLength(50)]
        public string? ModifiedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public Department Department { get; set; }
    }
}
