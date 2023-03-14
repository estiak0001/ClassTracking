using Business.Layer.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using GE = Global.Entity.ViewModels;

namespace ClassTracking.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherServices teacherServices;
        public TeacherController(ITeacherServices _teacherServices)
        {
            this.teacherServices = _teacherServices;
        }

        public IActionResult Index()
        {
            List<GE::TeacherVM> teacherinfo = new List<GE.TeacherVM>();
            return View(teacherinfo);
        }

        public IActionResult Create()
        {
            return View(new GE.TeacherVM());
        }

        public async Task<IActionResult> Edit(string Id)
        {
            GE::TeacherVM classInfo = await this.teacherServices.GetTeacherById(Convert.ToInt32(Id));
            return View("Create", classInfo);
        }

        public async Task<IActionResult> Save(GE::TeacherVM teacherinfo)
        {
            string Response = await this.teacherServices.Save(teacherinfo);
            return Json(Response);
        }
        public async Task<IActionResult> Remove(string Id)
        {
            string Response = await this.teacherServices.RemoveTeacher(Convert.ToInt32(Id));
            return Json(Response);
        }
        public async Task<IActionResult> GetAll()
        {
            var Response = await this.teacherServices.GetTeacher();
            return Json(Response);
        }
    }
}
