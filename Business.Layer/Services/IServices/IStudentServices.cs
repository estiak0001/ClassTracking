using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GE = Global.Entity.ViewModels;


namespace Business.Layer.Services.IServices
{
    public interface IStudentServices
    {
        Task<List<GE::StudentVM>> GetStudents();
        Task<GE::StudentVM> GetStudentById(int Id);
        Task<string> Save(GE::StudentVM teacher);
        Task<string> RemoveStudent(int Id);
    }
}
