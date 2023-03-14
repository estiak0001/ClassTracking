using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.AccessLayer.Models
{
    [Table(name: "ClassSchedule")]
    public class TblClassSchedule
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int DayNo { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        [ForeignKey("TblClassInformation")]
        public int ClassID { get; set; }
        public virtual TblClassInformation TblClassInformation { get; set; }
    }
}
