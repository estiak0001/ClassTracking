using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Entity.ViewModels
{
    public class TeachersSchedule
    {
        public int TeacherId { get; set; }
        public int SessionYear { get; set; }
        public int DayNo { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int? ClassId { get; set; }
    }
}
