
namespace IMSClassLibrary.Models
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; }
        [Required]
        public int ProjectId { get; set; }
     
        public string Comments  { get; set; }

        [Required]
        [StringLength(50)]
        public string? CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public User User { get; set; }
        public Project Project { get; set; }
        


    }
}
