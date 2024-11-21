

namespace IMSClassLibrary.Models
{
    public class ProfileModule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ProfileId { get; set; }

        [Required]
        public int ModuleId { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedBy { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [StringLength(100)]
        public string ModifiedBy { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime ModifiedDate { get; set; }

        public Profile Profile { get; set; }

        public Module Module { get; set; }
    }

}
