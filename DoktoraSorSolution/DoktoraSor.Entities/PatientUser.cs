using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoktoraSor.Entities
{
    [Table("PatientUsers")]
    public class PatientUser : MyEntityBase
    {
        [DisplayName("Ad"), StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Name { get; set; }
        [DisplayName("Soyad"), StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Surname { get; set; }
        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Username { get; set; }
        [DisplayName("E-posta"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(80, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Email { get; set; }
        [DisplayName("Şifre"), Required, StringLength(100)]
        public string Password { get; set; }
        [StringLength(60), ScaffoldColumn(false)] //images/upload/users/user_12.jpg
        public string ProfileImageFilename { get; set; }
        public bool IsActive { get; set; }
        [Required,ScaffoldColumn(false)]
        public Guid ActivateGuid { get; set; }
        public virtual List<Comment>Comments { get; set; }
        public virtual List<Liked>Likes { get; set; }
        public virtual List<DoctorLiked>DoctorLikes { get; set; }
        public virtual List<AppComment>AppComments { get; set; }
        public virtual List<Sales> Sales { get; set; }

        public PatientUser()
        {
            AppComments = new List<AppComment>();
        }
    }
}
