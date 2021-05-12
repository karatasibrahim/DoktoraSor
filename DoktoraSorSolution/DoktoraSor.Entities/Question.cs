using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoktoraSor.Entities
{ 
    [Table("Questions")]
   public class Question
    {   [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(300)]
        public string Text { get; set; } //soru metni
        public int AnswerId { get; set; } //sorunun cevabı
        public bool IsPayAccount { get; set; } //soru için ödeme yapılmış mı kontrolü/bakiyeden düşecek
        public DateTime QuestionTime { get; set; } //soru tarih ve zaman kontrolü
        public bool IsQuestionActive { get; set; } //soru aktif mi
        public string QuestionUploadFile { get; set; } //documents/users/userfiles/röntgen.dicom
        public virtual PatientUser PatientOwner { get; set; } //soru soran
        public virtual Answer Answer { get; set; }
    }
}
