

namespace IMSClassLibrary.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]

        [StringLength(50)]
        public string? Status { get; set; }

        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        [Required]
        [StringLength(100)]
        public string? CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;


        [StringLength(100)]
        public string? ModifiedBy { get; set; }
    }
}
