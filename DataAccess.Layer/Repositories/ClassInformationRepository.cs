using Data.AccessLayer.DBContext;
using Data.AccessLayer.Models;
using Data.AccessLayer.Repositories.IRepositories;
using Global.Entity;
using Global.Entity.ResponseModel;
using Global.Entity.ViewModels;
using Microsoft.EntityFrameworkCore;
using GE = Global.Entity.ViewModels;

namespace Data.AccessLayer.Repositories
{
    public class ClassInformationRepository : IClassInformationRepository
    {
        private readonly ClassTracking_DBContext DBContext;
        public ClassInformationRepository(ClassTracking_DBContext _DBContext)
        {
            this.DBContext = _DBContext;
        }

        public async Task<List<GE::ClassInformationVM>> GetClassInformation()
        {
            var _data = await this.DBContext.TblClassInformations.ToListAsync();
            List<GE::ClassInformationVM> classes = new List<GE::ClassInformationVM>();
            if (_data != null && _data.Count > 0)
            {
                _data.ForEach(item =>
                {
                    classes.Add(new GE.ClassInformationVM()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        standard = item.standard,
                        MaxStudent = item.MaxStudent,
                        //TotalStudent = DBContext.MobileRND_ChallanFile.Where(x => x.ChallanNo == history.DeliveryChallanNo).Select(p => p.FilePath).FirstOrDefault()
                        CreatedOn = item.CreatedOn,
                        UpdatedOn= item.UpdatedOn,
                        SessionYear= item.SessionYear,
                        TeacherID= item.TeacherID
                    });
                });
            }
            return classes;
        }

        public async Task<GE::ClassInformationVM> GetClassInformationbyId(int Id)
        {
            var _data = await this.DBContext.TblClassInformations.FirstOrDefaultAsync(item => item.Id == Id);
            GE::ClassInformationVM classes = new GE.ClassInformationVM();
            if (_data != null)
            {
                classes = (new GE.ClassInformationVM()
                {
                    Id = _data.Id,
                    Name = _data.Name,
                    standard = _data.standard,
                    MaxStudent = _data.MaxStudent,
                    //TotalStudent = DBContext.MobileRND_ChallanFile.Where(x => x.ChallanNo == history.DeliveryChallanNo).Select(p => p.FilePath).FirstOrDefault()
                    CreatedOn = _data.CreatedOn,
                    UpdatedOn = _data.UpdatedOn,
                    SessionYear = _data.SessionYear,
                    TeacherID = _data.TeacherID
                });
            }
            return classes;
        }

        public async Task<ResponseModel> Save(GE::ClassInformationVM classInformationVM)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                if (classInformationVM.Id > 0)
                {
                    var _exist = await this.DBContext.TblClassInformations.FirstOrDefaultAsync(item => item.Id == classInformationVM.Id);
                    if (_exist != null)
                    {
                        _exist.Name = classInformationVM.Name;
                        _exist.standard = classInformationVM.standard;
                        _exist.MaxStudent = (int)classInformationVM.MaxStudent;
                        _exist.TeacherID= classInformationVM.TeacherID;
                        _exist.SessionYear = (int)classInformationVM.SessionYear;
                        _exist.UpdatedOn= DateTime.Now;
                    }
                    await this.DBContext.SaveChangesAsync();
                    response.ResponseID = _exist.Id;
                    response.ReponseStatus = "pass";
                }
                else
                {
                    TblClassInformation _classInfo = new TblClassInformation()
                    {
                        Name = classInformationVM.Name,
                        standard = classInformationVM.standard,
                        MaxStudent = (int)classInformationVM.MaxStudent,
                        TeacherID = classInformationVM.TeacherID,
                        SessionYear = (int)classInformationVM.SessionYear,
                        CreatedOn= DateTime.Now
                    };
                    await this.DBContext.TblClassInformations.AddAsync(_classInfo);
                    await this.DBContext.SaveChangesAsync();
                    response.ResponseID = _classInfo.Id;
                    response.ReponseStatus = "pass";
                }
                
            }
            catch (Exception ex)
            {
                response.ReponseStatus = ex.Message;
            }
            return response;
        }


        public async Task<string> RemoveClassInformation(int Id)
        {
            var _data = await this.DBContext.TblClassInformations.FirstOrDefaultAsync(item => item.Id == Id);
            string Response = string.Empty;
            if (_data != null)
            {
                try
                {
                    this.DBContext.TblClassInformations.Remove(_data);
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