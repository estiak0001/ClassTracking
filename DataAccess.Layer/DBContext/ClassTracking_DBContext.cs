using Data.AccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.AccessLayer.DBContext
{
    public partial class ClassTracking_DBContext : DbContext
    {
        public ClassTracking_DBContext()
        {
        }
        public ClassTracking_DBContext(DbContextOptions<ClassTracking_DBContext> options)
                : base(options)
        {
        }

        public virtual DbSet<TblClassInformation> TblClassInformations { get; set; } = null!;
        public virtual DbSet<TblStudent> TblStudents { get; set; } = null!;
        public virtual DbSet<TblTeacher> TblTeacher { get; set; } = null!;
        public virtual DbSet<TblClassSchedule> TblClassSchedule { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
