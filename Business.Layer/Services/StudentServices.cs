using Business.Layer.Services.IServices;
using Data.AccessLayer.Repositories.IRepositories;
using Global.Entity;
using Global.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Layer.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly IStudentRepository studentRepository;
        public StudentServices(IStudentRepository _studentRepository)
        {
            this.studentRepository = _studentRepository;
        }

        public async Task<List<StudentVM>> GetStudents()
        {
            return await this.studentRepository.GetStudents();
        }

        public async Task<StudentVM> GetStudentById(int Id)
        {
            return await this.studentRepository.GetStudentbyId(Id);
        }

        public async Task<string> Save(StudentVM teacher)
        {
            return await this.studentRepository.Save(teacher);
        }

        public async Task<string> RemoveStudent(int Id)
        {
            return await this.studentRepository.RemoveStudent(Id);
        }
    }
}
