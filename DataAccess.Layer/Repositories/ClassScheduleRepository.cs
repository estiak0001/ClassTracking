using Data.AccessLayer.DBContext;
using Data.AccessLayer.Models;
using Data.AccessLayer.Repositories.IRepositories;
using Global.Entity.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GE = Global.Entity.ViewModels;

namespace Data.AccessLayer.Repositories
{
    public class ClassScheduleRepository : IClassScheduleRepository
    {
        private readonly ClassTracking_DBContext DBContext;
        public ClassScheduleRepository(ClassTracking_DBContext _DBContext)
        {
            this.DBContext = _DBContext;
        }

        public async Task<List<ClassScheduleVM>> GetClassSchedule()
        {
            var _data = await this.DBContext.TblClassSchedule.ToListAsync();
            List<GE::ClassScheduleVM> classSchedule = new List<GE::ClassScheduleVM>();
            if (_data != null && _data.Count > 0)
            {
                _data.ForEach(item =>
                {
                    classSchedule.Add(new GE.ClassScheduleVM()
                    {
                        Id = item.Id,
                        DayNo =item.DayNo,
                        StartTime = item.StartTime,
                        EndTime= item.EndTime,
                        ClassID= item.ClassID,
                        CreatedOn = item.CreatedOn,
                        UpdatedOn = item.UpdatedOn
                    });
                });
            }
            return classSchedule;
        }

        public async Task<ClassScheduleVM> GetClassScheduleById(int Id)
        {
            var _data = await this.DBContext.TblClassSchedule.FirstOrDefaultAsync(item => item.Id == Id);
            GE::ClassScheduleVM classSchedule = new GE.ClassScheduleVM();
            if (_data != null)
            {
                classSchedule = (new GE.ClassScheduleVM()
                {
                    Id = _data.Id,
                    DayNo= _data.DayNo,
                    StartTime  = _data.StartTime,
                    EndTime = _data.EndTime,
                    CreatedOn = _data.CreatedOn,
                    UpdatedOn = _data.UpdatedOn
                });
            }
            return classSchedule;
        }

        public async Task<string> Save(ClassScheduleVM classSchedule)
        {
            string Response = string.Empty;
            try
            {
                if (classSchedule.Id > 0)
                {
                    var _exist = await this.DBContext.TblClassSchedule.FirstOrDefaultAsync(item => item.Id == classSchedule.Id);
                    if (_exist != null)
                    {
                        _exist.DayNo = (int)classSchedule.DayNo;
                        _exist.StartTime = (TimeSpan)classSchedule.StartTime;
                        _exist.EndTime = (TimeSpan)classSchedule.EndTime;
                        _exist.ClassID = (int)classSchedule.ClassID;
                        _exist.UpdatedOn = DateTime.Now;
                    }
                }
                else
                {
                    TblClassSchedule _classSchdule = new TblClassSchedule()
                    {
                        DayNo = (int)classSchedule.DayNo,
                        StartTime = (TimeSpan)classSchedule.StartTime,
                        EndTime = (TimeSpan)classSchedule.EndTime,
                        ClassID = (int)classSchedule.ClassID,
                        CreatedOn = DateTime.Now
                    };  
                    await this.DBContext.TblClassSchedule.AddAsync(_classSchdule);
                }
                await this.DBContext.SaveChangesAsync();
                Response = "pass";
            }
            catch (Exception ex)
            {
                Response = ex.Message;
            }
            return Response;
        }

        public async Task<string> RemoveClassSchedule(int ClassId)
        {
            var _data = await this.DBContext.TblClassSchedule.Where(x=> x.ClassID == ClassId).ToListAsync();
            string Response = string.Empty;
            if (_data != null)
            {
                try
                {
                    this.DBContext.TblClassSchedule.RemoveRange(_data);
                    await this.DBContext.SaveChangesAsync();
                    Response = "pass";
                }
                catch (Exception ex)
                {

                }
            }
            return Response;
        }

        public async Task<List<TeachersSchedule>> CheckConflictAsync(ClassInformationVM classes)
        {
            List<TeachersSchedule> teachersSchedules = new List<TeachersSchedule>();
            var data = (from te in DBContext.TblTeacher
                        join classinfo in DBContext.TblClassInformations on new { x = te.Id } equals new { x = classinfo.TeacherID } into clas
                        from cl in clas
                        join classsche in DBContext.TblClassSchedule on new { x = cl.Id } equals new { x = classsche.ClassID } into clsche
                        from clschedule in clsche
                        select new TeachersSchedule()
                        {
                            TeacherId = te.Id,
                            SessionYear = (int)cl.SessionYear,
                            DayNo = (int)clschedule.DayNo,
                            StartTime = (TimeSpan)clschedule.StartTime,
                            EndTime = (TimeSpan)clschedule.EndTime,
                            ClassId = cl.Id
                        }).Where(x => x.TeacherId == classes.TeacherID).ToList();
            if(data != null)
            {
                teachersSchedules = data;
            }

            List<TeachersSchedule> conflists = new List<TeachersSchedule>();

            foreach (var dayNo in classes.DayList)
            {
                var isConflict = teachersSchedules.Where(x => (classes.Id == null || x.ClassId != classes.Id) && x.SessionYear == classes.SessionYear && x.DayNo == dayNo && (classes.StartTime >= x.StartTime || classes.StartTime <= x.EndTime) && (classes.EndTime >= x.StartTime || classes.EndTime <= x.EndTime)).Any();
                if (isConflict)
                {
                    TeachersSchedule conflict = new TeachersSchedule();
                    conflict.DayNo = dayNo;
                    conflict.StartTime = (TimeSpan)classes.StartTime;
                    conflict.EndTime = (TimeSpan)classes.EndTime;

                    conflists.Add(conflict);
                }
            }
            return conflists;
        }
    }
}
