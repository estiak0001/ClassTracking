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
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ClassTracking_DBContext DBContext;
        public TeacherRepository(ClassTracking_DBContext _DBContext)
        {
            this.DBContext = _DBContext;
        }

        public async Task<List<GE::TeacherVM>> GetTeachers()
        {
            var _data = await this.DBContext.TblTeacher.ToListAsync();
            List<GE::TeacherVM> teachers = new List<GE::TeacherVM>();
            if (_data != null && _data.Count > 0)
            {
                _data.ForEach(item =>
                {
                    teachers.Add(new GE.TeacherVM()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Designation = item.Designation,
                        CreatedOn = item.CreatedOn,
                        UpdatedOn = item.UpdatedOn
                    });
                });
            }
            return teachers;
        }

        public async Task<GE::TeacherVM> GetTeacherbyId(int Id)
        {
            var _data = await this.DBContext.TblTeacher.FirstOrDefaultAsync(item => item.Id == Id);
            GE::TeacherVM classes = new GE.TeacherVM();
            if (_data != null)
            {
                classes = (new GE.TeacherVM()
                {
                    Id = _data.Id,
                    Name = _data.Name,
                    Designation = _data.Designation,
                    CreatedOn = _data.CreatedOn,
                    UpdatedOn = _data.UpdatedOn
                });
            }
            return classes;
        }

        public async Task<string> Save(GE::TeacherVM teacher)
        {
            string Response = string.Empty;
            try
            {
                if (teacher.Id > 0)
                {
                    var _exist = await this.DBContext.TblTeacher.FirstOrDefaultAsync(item => item.Id == teacher.Id);
                    if (_exist != null)
                    {
                        _exist.Name = teacher.Name;
                        _exist.Designation = teacher.Designation;
                        _exist.UpdatedOn = DateTime.Now;
                    }
                }
                else
                {
                    TblTeacher _teachernfo = new TblTeacher()
                    {
                        Name = teacher.Name,
                        Designation = teacher.Designation,
                        CreatedOn = DateTime.Now
                    };
                    await this.DBContext.TblTeacher.AddAsync(_teachernfo);
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

        public async Task<string> RemoveTeacher(int Id)
        {
            var _data = await this.DBContext.TblTeacher.FirstOrDefaultAsync(item => item.Id == Id);
            string Response = string.Empty;
            if (_data != null)
            {
                try
                {
                    this.DBContext.TblTeacher.Remove(_data);
                    await this.DBContext.SaveChangesAsync();
                    Response = "pass";
                }
                catch (Exception ex)
                {

                }
            }
            return Response;
        }
    }
}
