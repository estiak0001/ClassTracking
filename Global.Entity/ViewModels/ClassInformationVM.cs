using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GE = Global.Entity.ViewModels;

namespace Global.Entity.ViewModels
{
    public class ClassInformationVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string standard { get; set; }
        public int? MaxStudent { get; set; }
        public int? SessionYear { get; set; }
        public int? TotalStudent { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int TeacherID { get; set; }
        public List<GE::TeacherVM> teachers { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int[] DayList { get; set; }
        public List<ClassScheduleVM> ClassSchedules { get; set; }
    }
}
