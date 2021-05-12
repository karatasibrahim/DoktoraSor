using DoktoraSor.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoktoraSor.DataAccessLayer.EntityFramework
{
   public class DatabaseContext:DbContext
    {
        public DbSet<DoctorUser> DoctorUsers { get; set; }
        public DbSet<PatientUser> PatientUsers { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category>Categories { get; set; }
        public DbSet<Liked> Likeds { get; set; }
        public DbSet<Answer>Answers { get; set; }
        public DbSet<ChatMessage>ChatMessages { get; set; }
        public DbSet<Degree>Degrees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DoctorLiked> DoctorLikes { get; set; }
        public DbSet<Expertise>Expertises { get; set; }
        public DbSet<Product>Products { get; set; }
        public DbSet<Question>Questions { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<AppComment> AppComments { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new MyInitializer());
        }

       // //FluentAPI KULLANIMI
       //// protected override void OnModelCreating(DbModelBuilder modelBuilder) //Database kurulurken ilişkisel veritabanından verilerin silinmesi işlemi cascade işlemlerinin tanımları yapılır.
       // {
       //     // FluentAPI 
       //     modelBuilder.Entity<Note>()  //note tablosunda
       //         .HasMany(n => n.Comments)  //comment tablosu ile işlişkilidir
       //         .WithRequired(c => c.Note) //commentin birden fazla notu olabilir ve zorunludur.
       //         .WillCascadeOnDelete(true);

       //     modelBuilder.Entity<Note>()  //note tablosunda
       //        .HasMany(n => n.Likes)  //like tablosu ile işlişkilidir
       //        .WithRequired(c => c.Note) //commentin birden fazla notu olabilir ve zorunludur.
       //        .WillCascadeOnDelete(true);
       // }
    }
}
