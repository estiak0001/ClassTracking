using Global.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GE = Global.Entity.ViewModels;

namespace Data.AccessLayer.Repositories.IRepositories
{
    public interface IClassScheduleRepository
    {
        Task<List<GE::ClassScheduleVM>> GetClassSchedule();
        Task<GE::ClassScheduleVM> GetClassScheduleById(int Id);
        Task<string> Save(GE::ClassScheduleVM classSchedule);
        Task<string> RemoveClassSchedule(int Id);

        Task<List<TeachersSchedule>> CheckConflictAsync(ClassInformationVM classes);
    }
}
