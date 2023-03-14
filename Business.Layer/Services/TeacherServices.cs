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
    public class TeacherServices : ITeacherServices
    {
        private readonly ITeacherRepository teacherRepository;
        public TeacherServices(ITeacherRepository _teacherRepository)
        {
            this.teacherRepository = _teacherRepository;
        }

        public async Task<List<TeacherVM>> GetTeacher()
        {
            return await this.teacherRepository.GetTeachers();
        }

        public async Task<TeacherVM> GetTeacherById(int Id)
        {
            return await this.teacherRepository.GetTeacherbyId(Id);
        }

        public async Task<string> Save(TeacherVM teacher)
        {
            return await this.teacherRepository.Save(teacher);
        }

        public async Task<string> RemoveTeacher(int Id)
        {
            return await this.teacherRepository.RemoveTeacher(Id);
        }
    }
}
