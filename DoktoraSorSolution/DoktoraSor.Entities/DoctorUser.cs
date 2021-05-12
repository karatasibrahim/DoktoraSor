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
    [Table("DoctorUsers")]
    public class DoctorUser:MyEntityBase
    {
        [DisplayName("Ad"), StringLength(25, ErrorMessage ="{0} alanı max. {1} karakter olmalıdır.")]
        public string Name { get; set; }
        [DisplayName("Soyad"), StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Surname { get; set; }
        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage ="{0} alanı gereklidir."), StringLength(25)]
        public string Username { get; set; }
        [DisplayName("E-posta"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(80, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Email { get; set; }
        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")] //şifreleme işlemleri için karakter uzun tutulmuştur.
        public string Password { get; set; }
        [StringLength(60), ScaffoldColumn(false)] //images/users/userimages/user_12.jpg
        public string ProfileImageFilename { get; set; }
        [DisplayName("Diploma No"), StringLength(8)]
        public string DipNumber { get; set; } //Diploma No
        [DisplayName("Tescil No"), StringLength(8)]
        public string RegistrationNumber { get; set; } //Tescil No
        [DisplayName("Biografi"), UIHint("MultilineText")]        
        public string ProfileBio { get; set; }
        [DisplayName("Aktif")]
        public bool IsActive { get; set; }
        [DisplayName("Admin")]
        public bool IsAdmin { get; set; }
        [Required, ScaffoldColumn(false)]
        public Guid ActivateGuid { get; set; }
        [DisplayName("Beğenilme Sayısı")]
        public int DoctorLikeCount { get; set; }
        public virtual List<Note> Notes { get; set; } //kullanıcının birden fazla notu olabilir.Blog/Post
        public virtual List<Comment> Comments { get; set; } //birden fazla yorum yapar
        public virtual List<Liked> Likes { get; set; }
        public virtual List<DoctorLiked> DoctorLikeds { get; set; }
        public virtual List<Sales>Sales { get; set; }
        public virtual Department Department { get; set; }
        public virtual Degree Degree { get; set; }
       
        public DoctorUser()
        {
            Comments = new List<Comment>();
            DoctorLikeds = new List<DoctorLiked>();
        }
    }
}
