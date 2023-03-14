using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Entity.ViewModels
{
    public class StudentVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public string Nationality { get; set; }
        public DateTime EnrollDate { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
