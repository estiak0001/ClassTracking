using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GE = Global.Entity.ViewModels;
using RS = Global.Entity.ResponseModel;

namespace Data.AccessLayer.Repositories.IRepositories
{
    public interface IClassInformationRepository
    {
        Task<List<GE::ClassInformationVM>> GetClassInformation();
        Task<GE::ClassInformationVM> GetClassInformationbyId(int Id);
        Task<RS::ResponseModel> Save(GE::ClassInformationVM classInformationVM);
        Task<string> RemoveClassInformation(int Id);

    }
}
