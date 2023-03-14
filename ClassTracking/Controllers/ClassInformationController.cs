using Business.Layer.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using GE = Global.Entity.ViewModels;

namespace ClassTracking.Controllers
{
    public class ClassInformationController : Controller
    {
        private readonly IClassInformationServices classInformation;
        private readonly ITeacherServices teacherServices;
        private readonly IClassScheduleService classScheduleService;
        public ClassInformationController(IClassInformationServices _classInformation, ITeacherServices _teacherServices)
        {
            this.classInformation = _classInformation;
            this.teacherServices = _teacherServices;
        }

        public IActionResult Index()
        {
            List<GE::ClassInformationVM> classInfo = new List<GE.ClassInformationVM>();
            return View(classInfo);
        }

        public async Task<IActionResult> Create()
        {
            GE.ClassInformationVM classInfo = new GE.ClassInformationVM();
            var teacherList = await this.teacherServices.GetTeacher();
            if(teacherList != null)
            {
                classInfo.teachers = teacherList;
            }
            else
            {
                classInfo.teachers = new List<GE.TeacherVM>();
            }
            //GE.ClassScheduleVM schedule = new GE.ClassScheduleVM();
            //classInfo.classSchedule = new GE.ClassScheduleVM();
            return View(classInfo);
        }

        public async Task<IActionResult> Edit(string Id)
        {
            GE::ClassInformationVM classInfo = await this.classInformation.GetClassInformationById(Convert.ToInt32(Id));
            var teacherList = await this.teacherServices.GetTeacher();
            if (teacherList != null)
            {
                classInfo.teachers = teacherList;
            }
            else
            {
                classInfo.teachers = new List<GE.TeacherVM>();
            }
            return View("Create", classInfo);
        }

        public async Task<IActionResult> Save(GE::ClassInformationVM classInfo)
        {
            var Response = await this.classInformation.Save(classInfo);
            return Json(Response);
        }
        public async Task<IActionResult> Remove(string Id)
        {
            string Response = await this.classInformation.RemoveClassInformations(Convert.ToInt32(Id));
            return Json(Response);
        }
        public async Task<IActionResult> GetAll()
        {
            var Response = await this.classInformation.GetClassInformation();
            return Json(Response);
        }

        public async Task<IActionResult> GetConflict(GE::ClassInformationVM classInfo)
        {
            var Response = await this.classInformation.CheckConflictAsync(classInfo);
            return Json(Response);
        }
    }
}
