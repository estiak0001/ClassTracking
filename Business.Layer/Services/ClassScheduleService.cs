using Business.Layer.Services.IServices;
using Data.AccessLayer.Repositories.IRepositories;
using Global.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Layer.Services
{
    public class ClassScheduleService : IClassScheduleService
    {
        private readonly IClassScheduleRepository classScheduleRepository;
        private readonly ITeacherRepository teacherRepository;
        private readonly IClassInformationRepository classInformationRepository;
        public ClassScheduleService(IClassScheduleRepository _classScheduleRepository, 
            ITeacherRepository _teacherRepository, 
            IClassInformationRepository _classInformationRepository)
        {
            this.classScheduleRepository = _classScheduleRepository;
            this.teacherRepository = _teacherRepository;
            this.classInformationRepository = _classInformationRepository;
        }

        public async Task<List<ClassScheduleVM>> GetClassSchedule()
        {
            return await this.classScheduleRepository.GetClassSchedule();
        }

        public async Task<ClassScheduleVM> GetClassScheduleById(int Id)
        {
            return await this.classScheduleRepository.GetClassScheduleById(Id);
        }

        public async Task<string> RemoveClassSchedule(int Id)
        {
            return await this.classScheduleRepository.RemoveClassSchedule(Id);
        }

        public async Task<string> Save(ClassScheduleVM classSchedule)
        {
            return await this.classScheduleRepository.Save(classSchedule);
        }

        public async Task<List<TeachersSchedule>> GetClassScheduleByTeacherID(int TeacherID)
        {
            var teacherInfo = await this.teacherRepository.GetTeachers();
            var classInfo = await this.classInformationRepository.GetClassInformation();
            var classSchedule = await this.classScheduleRepository.GetClassSchedule();

            List<TeachersSchedule> teachersSchedules= new List<TeachersSchedule>(); 
            var data = (from te in teacherInfo
                        join classinfo in classInfo on new { x = te.Id } equals new { x = classinfo.TeacherID } into clas
                        from cl in clas
                        join classsche in classSchedule on new { x = cl.Id } equals new { x = classsche.ClassID } into clsche
                        from clschedule in clsche
                        select new TeachersSchedule()
                        {
                            TeacherId = te.Id,
                            SessionYear = (int)cl.SessionYear,
                            DayNo = (int)clschedule.DayNo,
                            StartTime = (TimeSpan)clschedule.StartTime,
                            EndTime = (TimeSpan)clschedule.EndTime,
                            ClassId = cl.Id
                        }).Where(x => x.TeacherId == TeacherID).ToList();
            if(data != null)
            {
                teachersSchedules = data;
            }
            return teachersSchedules;
        }
    }
}
