using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DoktoraSor.Entities;

namespace DoktoraSor.DataAccessLayer.EntityFramework
{
   public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>  //Database yokken çalışacak
    {
         //DatabaseContext Initializer override işlemi
        protected override void Seed(DatabaseContext context) //Seed in içerisinde örnek kullanıcılar yer almaktadır.
        { //Admin User ekleme
            DoctorUser admin = new DoctorUser()
            {
                Name = "İbrahim",
                Surname = "Karataş",
                Email = "ibrahimkaratas@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "ibrahimkaratas",
                ProfileImageFilename = "default.jpg",
                Password="123456",
                CreatedOn=DateTime.Now,
                ModifiedOn=DateTime.Now.AddMinutes(5),
                ModifiedUserName="system"
            };
            //Standart DoktorUser ekleme
            DoctorUser standartUser = new DoctorUser()
            {
                Name = "Gülsüm",
                Surname = "Karataş",
                Email = "gulsumkaratas@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                Username = "gulsumkaratas",
                Password = "123456",
                ProfileImageFilename = "default.jpg",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUserName = "system"
            };
            //PatientUserActive ekleme
            PatientUser patientUserActive = new PatientUser()
            {
                Name = "Mehmet",
                Surname = "Tunçkın",
                Email = "mehmettunckin@hotmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                Username = "mehmettunckin",
                ProfileImageFilename = "default.jpg",
                Password ="123456",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUserName = "system"
            };
            //PatientUserNotActive ekleme
            PatientUser patientUserNotActive = new PatientUser()
            {
                Name = "Ayşe",
                Surname = "Akçay",
                Email = "ayseakcay@mail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = false,
                Username = "ayseakcay",
                ProfileImageFilename = "default.jpg",
                Password = "123456",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(65),
                ModifiedUserName = "system"
            };


            context.DoctorUsers.Add(admin);
            context.DoctorUsers.Add(standartUser);
            context.PatientUsers.Add(patientUserActive);
            context.PatientUsers.Add(patientUserNotActive);

            // Standart Doktor profili ekleme 8 kullanıcı
            for (int i = 0; i < 8; i++)
            {
                DoctorUser user = new DoctorUser()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname =FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ProfileImageFilename = "default.jpg",
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = false,
                    DoctorLikeCount=FakeData.NumberData.GetNumber(1,9),
                    Username = $"user{i}", //kullanıcı adı i kadar dönsün. user0, user1, user2.......user8
                    Password = "123",
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedUserName = $"user{ i }"
                };
                //Hasta profili ekleme
                PatientUser patientUser = new PatientUser()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ProfileImageFilename = "default.jpg",
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    Username = $"patientUser{i}",
                    Password = "123",
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedUserName = $"patientUser{i}"
                };

                context.DoctorUsers.Add(user);
                context.PatientUsers.Add(patientUser);

              
                
            }
            context.SaveChanges();
            List<PatientUser> userlist = context.PatientUsers.ToList();
            List<DoctorUser> duserlist = context.DoctorUsers.ToList();
            // FakeData Category ekleme
            for (int i = 0; i < 10; i++)
            {
                Category cat = new Category()
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifiedUserName = "system"
                };

                AppComment appComment = new AppComment()
                {
                    Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 2)),
                    IsApproval = true,
                    PatientOwner= userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)]
                };
                context.AppComments.Add(appComment);
                context.Categories.Add(cat); //üretilen 10 kategori ilgili dbSet e eklenir

                // FakeData Notes ekleme
                for (int k = 0; k < FakeData.NumberData.GetNumber(5,9); k++) //Her bir kategori için 5 ile 9 arasında Fakedata ile note üretilecek
                {
                    DoctorUser owner = duserlist[FakeData.NumberData.GetNumber(0, duserlist.Count - 1)];
                    Note note = new Note()
                    {
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        IsDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(1, 9),
                        DoctorOwner = owner,
                        CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUserName = owner.Username,
                    };

                    cat.Notes.Add(note); //Kategorinin notları eklendi

                    // FakeData Comments ekleme

                    for (int j = 0; j < FakeData.NumberData.GetNumber(3,5); j++) //notun commentleri
                    {
                        DoctorUser comment_Downer = duserlist[FakeData.NumberData.GetNumber(0, duserlist.Count - 1)];
                       PatientUser comment_Powner=userlist[FakeData.NumberData.GetNumber(0, duserlist.Count - 1)];

                        Comment comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentence(),                            
                            PatientOwner= comment_Powner,
                            DoctorOwner = comment_Downer,
                            CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedUserName = comment_Powner.Username
                        };
                        note.Comments.Add(comment);
                        
                    }
                     
                    // FakeData Likes ekleme
                   
                    //notun likeları
                    for (int l = 0; l < note.LikeCount; l++)
                    {
                        Liked liked = new Liked()
                        {
                            LikedPatientUser = userlist[l],
                            LikedDoctorUser = duserlist[l]
                            
                        };

                        DoctorLiked doctorLiked = new DoctorLiked()
                        {
                            LikedPatientUser = userlist[l]
                            
                        };


                        note.Likes.Add(liked); //notun like countuna ekle
                        owner.DoctorLikeds.Add(doctorLiked);
                    }

                  
                    
                }
            }

            context.SaveChanges();
        }
    }
}
