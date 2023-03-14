using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GE = Global.Entity.ViewModels;

namespace Business.Layer.Services.IServices
{
    public interface ITeacherServices
    {
        Task<List<GE::TeacherVM>> GetTeacher();
        Task<GE::TeacherVM> GetTeacherById(int Id);
        Task<string> Save(GE::TeacherVM teacher);
        Task<string> RemoveTeacher(int Id);
    }
}
