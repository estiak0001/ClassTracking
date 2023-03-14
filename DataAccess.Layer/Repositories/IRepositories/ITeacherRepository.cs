using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GE = Global.Entity.ViewModels;
namespace Data.AccessLayer.Repositories.IRepositories
{
    public interface ITeacherRepository
    {
        Task<List<GE::TeacherVM>> GetTeachers();
        Task<GE::TeacherVM> GetTeacherbyId(int Id);
        Task<string> Save(GE::TeacherVM teacher);
        Task<string> RemoveTeacher(int Id);
    }
}
