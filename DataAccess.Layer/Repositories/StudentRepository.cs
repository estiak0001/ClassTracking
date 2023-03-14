using Data.AccessLayer.Repositories.IRepositories;
using GE = Global.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data.AccessLayer.Models;
using Global.Entity.ViewModels;
using Data.AccessLayer.DBContext;

namespace Data.AccessLayer.Repositories
{
    internal class StudentRepository : IStudentRepository
    {
        private readonly ClassTracking_DBContext DBContext;
        public StudentRepository(ClassTracking_DBContext _DBContext)
        {
            this.DBContext = _DBContext;
        }

        public async Task<List<GE::StudentVM>> GetStudents()
        {
            var _data = await this.DBContext.TblStudents.ToListAsync();
            List<GE::StudentVM> students = new List<GE::StudentVM>();
            if (_data != null && _data.Count > 0)
            {
                _data.ForEach(item =>
                {
                    students.Add(new GE.StudentVM()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        MotherName = item.MotherName,
                        FatherName = item.FatherName,
                        Nationality= item.Nationality,
                        EnrollDate= item.EnrollDate,    
                        CreatedOn = item.CreatedOn,
                        UpdatedOn = item.UpdatedOn
                    });
                });
            }
            return students;
        }

        public async Task<GE::StudentVM> GetStudentbyId(int Id)
        {
            var _data = await this.DBContext.TblStudents.FirstOrDefaultAsync(item => item.Id == Id);
            GE::StudentVM students = new GE.StudentVM();
            if (_data != null)
            {
                students = (new GE.StudentVM()
                {
                    Id = _data.Id,
                    Name = _data.Name,
                    FatherName = _data.FatherName,
                    MotherName = _data.MotherName,
                    Nationality = _data.Nationality,
                    EnrollDate= _data.EnrollDate,
                    CreatedOn = _data.CreatedOn,
                    UpdatedOn = _data.UpdatedOn
                });
            }
            return students;
        }

        public async Task<string> Save(GE::StudentVM student)
        {
            string Response = string.Empty;
            try
            {
                if (student.Id > 0)
                {
                    var _exist = await this.DBContext.TblStudents.FirstOrDefaultAsync(item => item.Id == student.Id);
                    if (_exist != null)
                    {
                        _exist.Name = student.Name;
                        _exist.MotherName = student.MotherName;
                        _exist.FatherName = student.FatherName;
                        _exist.Nationality = student.Nationality;
                        _exist.EnrollDate = student.EnrollDate;
                        _exist.UpdatedOn = DateTime.Now;
                    }
                }
                else
                {
                    TblStudent _studentinfo = new TblStudent()
                    {
                        Name = student.Name,
                        MotherName = student.MotherName,
                        FatherName = student.FatherName,
                        Nationality = student.Nationality,
                        EnrollDate = student.EnrollDate,
                        CreatedOn = DateTime.Now
                    };
                    await this.DBContext.TblStudents.AddAsync(_studentinfo);
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

        public async Task<string> RemoveStudent(int Id)
        {
            var _data = await this.DBContext.TblStudents.FirstOrDefaultAsync(item => item.Id == Id);
            string Response = string.Empty;
            if (_data != null)
            {
                try
                {
                    this.DBContext.TblStudents.Remove(_data);
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
