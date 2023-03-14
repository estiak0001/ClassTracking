using Global.Entity.ResponseModel;
using Global.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GE = Global.Entity.ViewModels;

namespace Business.Layer.Services.IServices
{
    public interface IClassInformationServices
    {
        Task<List<GE::ClassInformationVM>> GetClassInformation();
        Task<GE::ClassInformationVM> GetClassInformationById(int Id);
        Task<ResponseModel> Save(GE::ClassInformationVM classes);
        Task<string> RemoveClassInformations(int Id);

        Task<List<TeachersSchedule>> CheckConflictAsync(ClassInformationVM classes);
    }
}
