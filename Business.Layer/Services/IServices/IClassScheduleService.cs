using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GE = Global.Entity.ViewModels;


namespace Business.Layer.Services.IServices
{
    public interface IClassScheduleService
    {
        Task<List<GE::ClassScheduleVM>> GetClassSchedule();
        Task<GE::ClassScheduleVM> GetClassScheduleById(int Id);
        Task<string> Save(GE::ClassScheduleVM classSchedule);
        Task<string> RemoveClassSchedule(int Id);

        Task<List<GE::TeachersSchedule>> GetClassScheduleByTeacherID(int TeacherID);
    }
}
