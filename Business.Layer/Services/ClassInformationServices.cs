using Business.Layer.Services.IServices;
using Data.AccessLayer.Repositories;
using Global.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.AccessLayer.Repositories.IRepositories;
using Global.Entity.ResponseModel;
using Global.Entity;

namespace Business.Layer.Services
{
    public class ClassInformationServices : IClassInformationServices
    {
        private readonly IClassInformationRepository classInformation;
        private readonly IClassScheduleRepository classSchedule;
        private readonly IClassScheduleService classScheduleService;
        
        public ClassInformationServices(IClassInformationRepository _classInformation, IClassScheduleRepository _classScheduleRepository, IClassScheduleService _classScheduleService)
        {
            this.classInformation = _classInformation;
            this.classSchedule = _classScheduleRepository;
            this.classScheduleService= _classScheduleService;
        }

        public async Task<List<ClassInformationVM>> GetClassInformation()
        {
            return await this.classInformation.GetClassInformation();
        }

        public async Task<ClassInformationVM> GetClassInformationById(int Id)
        {
            var classInfo = await this.classInformation.GetClassInformationbyId(Id);
            var scheduleInfo = await this.classSchedule.GetClassSchedule();
            var classScheduleByClasId = scheduleInfo.Where(x=> x.ClassID == classInfo.Id).ToList();
            classInfo.DayList = (classScheduleByClasId.Select(p=> (int)p.DayNo).ToArray());
            classInfo.StartTime= classScheduleByClasId.Select(p=> p.StartTime).FirstOrDefault();
            classInfo.EndTime = classScheduleByClasId.Select(p => p.EndTime).FirstOrDefault();
            return classInfo;
        }
        public async Task<ResponseModel> Save(ClassInformationVM classes)
        {
            var isConflict = await this.classSchedule.CheckConflictAsync(classes);
            if(isConflict.Count > 0)
            {
                ResponseModel response = new ResponseModel();
                response.ReponseStatus = "Teacher Schedule Conflict Please check";
                return response;
            }
            else
            {
                var classInfoSubmit = await this.classInformation.Save(classes);

                if (classInfoSubmit.ReponseStatus == "pass")
                {
                    await this.classSchedule.RemoveClassSchedule((int)classInfoSubmit.ResponseID);

                    foreach (var dayNo in classes.DayList)
                    {
                        ClassScheduleVM classSchedule = new ClassScheduleVM();
                        classSchedule.DayNo = dayNo;
                        classSchedule.StartTime = classes.StartTime;
                        classSchedule.EndTime = classes.EndTime;
                        classSchedule.ClassID = (int)classInfoSubmit.ResponseID;
                        await this.classSchedule.Save(classSchedule);
                    }
                }

                return classInfoSubmit;
            }
            
        }

        public async Task<List<TeachersSchedule>> CheckConflictAsync(ClassInformationVM classes)
        {
            return await this.classSchedule.CheckConflictAsync(classes); 
        }

        public async Task<string> RemoveClassInformations(int Id)
        {
            return await this.classInformation.RemoveClassInformation(Id);
        }
    }
}
