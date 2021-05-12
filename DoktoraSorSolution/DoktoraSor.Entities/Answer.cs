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
    [Table("Answers")]
   public class Answer 
    {  [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Soru"),Required,StringLength(300, ErrorMessage ="{0} alanı max. {1} karakter olmalıdır.")]
        public string Text { get; set; } //cevap metni
        public bool IsPayAccount { get; set; } //cevap için ödeme yapılmış mı kontrolü/bakiyeden düşecek
        public DateTime AnswerTime { get; set; } //cevap tarih ve zaman kontrolü
        public bool IsAnswerActive { get; set; } //cevap aktif mi
        public virtual DoctorUser DoctorOwner { get; set; } //cevap veren 
      

    }
}
