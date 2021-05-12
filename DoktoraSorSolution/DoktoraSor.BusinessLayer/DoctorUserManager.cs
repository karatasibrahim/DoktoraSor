using DoktoraSor.BusinessLayer.Abstract;
using DoktoraSor.BusinessLayer.Results;
using DoktoraSor.Common.Helpers;
using DoktoraSor.DataAccessLayer.EntityFramework;
using DoktoraSor.Entities;
using DoktoraSor.Entities.Messages;
using DoktoraSor.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoktoraSor.BusinessLayer
{
   public class DoctorUserManager: ManagerBase<DoctorUser>
    {
        
        public BusinessLayerResult<DoctorUser> RegisterDoctorUser(RegisterViewModel data)
        {

            DoctorUser user=  Find(x => x.Username == data.Username || x.Email==data.Email); //data dan gelen username ile veri kontrolü (Kullanıcı kayıtlı mı değil mi veritabanından kontrolünü yapıyoruz.)
            BusinessLayerResult<DoctorUser> res = new BusinessLayerResult<DoctorUser>();
            if (user!=null)
            {
                if (user.Username==data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı");
                }
                if (user.Email==data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi adı kayıtlı");
                }
            }
            else
            {
              int dbResult=base.Insert(new DoctorUser()
                {
                    Username=data.Username,
                    Email=data.Email,
                    Password=data.Password,
                    ProfileImageFilename = "default.jpg",
                  ActivateGuid =Guid.NewGuid(),                    
                    IsActive =false,
                    IsAdmin=false
                    
                });

                if (dbResult>0)
                {
                   res.Result= Find(x => x.Email == data.Email && x.Username == data.Username);
                    //Mail gönderme işlemi
                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}/Home/UserActivate/{res.Result.ActivateGuid}";
                    string body= $"Merhaba {res.Result.Username}; <br/> <br/> Doktora Sor Uygulama aktifleştirme mailini aldınız. <br/><br/>" +
                        $"Lütfen Hesabınızı aktifleştirmek için <a href='{activateUri}' target='_blank'>tıklayınız.</a>";
                    MailHelper.SendMail(body, res.Result.Email,"Doktora Sor Hesap Aktifleştirme");
                }

            }
            return res;


        }

        public BusinessLayerResult<DoctorUser> UpdateProfile(DoctorUser data)
        {
            DoctorUser user = Find(x => x.Id == data.Id && (x.Username==data.Username || x.Email == data.Email));
            BusinessLayerResult<DoctorUser> res = new BusinessLayerResult<DoctorUser>();
          

            if (user != null && user.Id != data.Id)
            {
                if (user.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı");
                }
                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı");

                }
            }
            else
            {
                res.Result = Find(x => x.Id == data.Id);
                res.Result.Email = data.Email;
                res.Result.Name = data.Name;
                res.Result.Surname = data.Surname;
                res.Result.Password = data.Password;
                res.Result.Username = data.Username;
                res.Result.DipNumber = data.DipNumber;
                res.Result.RegistrationNumber = data.RegistrationNumber;
                res.Result.ProfileBio = data.ProfileBio;
                

                if (string.IsNullOrEmpty(data.ProfileImageFilename) == false)
                {
                    res.Result.ProfileImageFilename = data.ProfileImageFilename;
                }
                if (base.Update(res.Result) == 0)
                {
                    res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Profil güncellenemedi");
                }
            }
            return res;
        }

        public BusinessLayerResult<DoctorUser> GetUserById(int id)
        {
            BusinessLayerResult<DoctorUser> res = new BusinessLayerResult<DoctorUser>();
            res.Result = Find(x => x.Id == id);
            if (res.Result==null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı bulunamadı");
            }
            return res;
        }

        public BusinessLayerResult<DoctorUser> LoginDoctorUser(LoginViewModel data)
        {
            BusinessLayerResult<DoctorUser> res = new BusinessLayerResult<DoctorUser>();
            res.Result = Find(x => x.Username == data.Username && x.Password == data.Password); 
        
           

            if (res.Result != null)
            {
                if (res.Result.IsActive==false)
                {
                    res.AddError(ErrorMessageCode.UserIsNotActive, "Kullanıcı aktifleştirilmemiştir.");
                    res.AddError(ErrorMessageCode.CheckYourEmail, "Lütfen e-posta adresinizi kontrol ediniz.");

                    
                }
               
            }
            else
            {
                res.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanıcı adı ya da şifre uyuşmuyor.");
               
            }
            return res;




            //yönlendirme
            //Session
        }

        public BusinessLayerResult<DoctorUser> ActivateUser(Guid activateId)
        {
            BusinessLayerResult<DoctorUser> res = new BusinessLayerResult<DoctorUser>();
            res.Result = Find(x => x.ActivateGuid==activateId);

            if (res.Result!=null)
            {
                if (res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserAlreadyActive, "Kullanıcı zaten aktif edilmiştir.");
                    return res;
                }
                res.Result.IsActive = true;
                Update(res.Result);
            }
            else
            {
                res.AddError(ErrorMessageCode.ActivateIdDoesNotExists, "Aktifleştirilecek kullanıcı kaydı bulunamadı.");
            }
            return res;
        }

        public new BusinessLayerResult<DoctorUser> Insert(DoctorUser data) //Override işlemine benzer. Base den gelen metodu gizledik. Method hiding işlemi //data dan gelen username ile veri kontrolü (Kullanıcı kayıtlı mı değil mi veritabanından kontrolünü yapıyoruz.)
        {
            DoctorUser user = Find(x => x.Username == data.Username || x.Email == data.Email); 
            BusinessLayerResult<DoctorUser> res = new BusinessLayerResult<DoctorUser>();

            res.Result = data;
            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı");
                }
                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi adı kayıtlı");
                }
            }
            else
            {
                res.Result.ProfileImageFilename = "default.jpg";
                res.Result.ActivateGuid = Guid.NewGuid();
                
              if(   base.Insert(res.Result)==0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotInserted, "Kullanıcı kayıt edilemedi.");
                }

                

            }
            return res;
        }

        public new BusinessLayerResult<DoctorUser> Update(DoctorUser data)
        {
            DoctorUser user = Find(x => x.Username == data.Username || x.Email == data.Email);
            BusinessLayerResult<DoctorUser> res = new BusinessLayerResult<DoctorUser>();
            res.Result = data;

            if (user != null && user.Id != data.Id)
            {
                if (user.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı");
                }
                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı");

                }
            }
            else
            {
                res.Result = Find(x => x.Id == data.Id);
                res.Result.Email = data.Email;
                res.Result.Name = data.Name;
                res.Result.Surname = data.Surname;
                res.Result.Password = data.Password;
                res.Result.Username = data.Username;
                res.Result.DipNumber = data.DipNumber;                
                res.Result.RegistrationNumber = data.RegistrationNumber;
                res.Result.IsActive = data.IsActive;
                res.Result.IsAdmin = data.IsAdmin;
                res.Result.ProfileBio = data.ProfileBio;
 
                if (base.Update(res.Result) == 0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotUpdated, "Kullanıcı güncellenemedi");
                }
            }
            return res;
        }

        public BusinessLayerResult<DoctorUser> RemoveUserById(int id)
        {
            BusinessLayerResult<DoctorUser> res = new BusinessLayerResult<DoctorUser>();
            DoctorUser duser = Find(x => x.Id == id);
            if (duser != null)
            {
                if (Delete(duser) == 0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotRemove, "Kullanıcı Silinemedi.");
                    return res;

                }

            }
            else
            {
                res.AddError(ErrorMessageCode.UserCouldNotFind, "Kullanıcı Bulunamadı.");
            }
            return res;
        }


    }


}
