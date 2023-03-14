using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GE = Global.Entity.ViewModels;
namespace Data.AccessLayer.Repositories.IRepositories
{
    public interface IStudentRepository
    {
        Task<List<GE::StudentVM>> GetStudents();
        Task<GE::StudentVM> GetStudentbyId(int Id);
        Task<string> Save(GE::StudentVM student);
        Task<string> RemoveStudent(int Id);
    }
}
