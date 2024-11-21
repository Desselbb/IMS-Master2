using System;
using System.Collections.Generic;
using System.Linq;


namespace IMSClassLibrary.Models
{
    public class Module
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
             
        public int ParentId { get; set; }

        [Required]
        [StringLength(60)]
        public string? Name { get; set; }


        [Required]
        [StringLength(200)]
        public string? Url { get; set; }


        [Required]
        [StringLength(50)]
        public string? Icon { get; set; }

        public int Ordering { get; set; }


        [StringLength(50)]
        public string? Status { get; set; }


        [Required]
        [StringLength(50)]
        public string? CreatedBy { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;


        [StringLength(50)]
        public string? ModifiedBy { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime ModifiedDate { get; set; } = DateTime.Now;


    }

}

