using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.AccessLayer.Models
{
    [Table(name: "ClassInformation")]
    public class TblClassInformation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(10)]
        public string standard { get; set; }
        [Required]
        public int MaxStudent { get; set; } //= 60;
        [Required]
        public int SessionYear { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        [ForeignKey("TblTeacher")]
        public int TeacherID { get; set; }
        public virtual TblTeacher TblTeacher { get; set; }
    }
}
