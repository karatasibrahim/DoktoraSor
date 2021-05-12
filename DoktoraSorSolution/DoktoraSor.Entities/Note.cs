using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoktoraSor.Entities
{ [Table("Notes")]
   public class Note:MyEntityBase
    {
        [DisplayName("Başlık"),Required,StringLength(60, ErrorMessage ="{0} alanı max. {1} karakter olmalıdır.")]
        public string Title { get; set; } //başlık
        [DisplayName("İçerik"),Required, StringLength(2000, ErrorMessage ="{0} alanı max. {1} karakter olmalıdır.")]
        public string Text { get; set; } //içerik
        [DisplayName("Taslak")]
        public bool IsDraft { get; set; } //taslak/yaynlanmış mı kontrolü
        [DisplayName("Beğenilme")]
        public int LikeCount { get; set; } //beğenenlerin sayısı
        [DisplayName("Kategori")]
        public int CategoryId { get; set; }
        public virtual DoctorUser DoctorOwner { get; set; } //note sadece bir sahibi/kullanıcısı var
        public virtual Category Category { get; set; } //her notun bir kategorisi vardır
        public virtual List<Comment> Comments { get; set; } //notun birden çok yorumu vardır
        public virtual List<Liked> Likes { get; set; } //bir notun birden çok like vardır
        public Note() //comment listesinin otomatik oluşmasını sağlamak için yapılır. Hata almamak maksadıyla
        {
            Comments = new List<Comment>();
            Likes = new List<Liked>();
        }
    }
}
