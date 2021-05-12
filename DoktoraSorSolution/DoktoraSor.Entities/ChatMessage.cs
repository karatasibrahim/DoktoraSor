using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoktoraSor.Entities
{ [Table("ChatMessages")]
   public class ChatMessage:MyEntityBase
    {   [DisplayName("Mesaj"),Required,StringLength(300, ErrorMessage ="{0} alanı max. {1} karakter olmalıdır.")]
        public string Text { get; set; } //mesaj içeriği
        public bool IsPayAccount { get; set; } //ödeme kontrolü
        public DateTime MessageTime { get; set; } //mesajın zaman sınırlama kontrolünün yapılması
        public bool IsMessageActive { get; set; } //mesaj içeriğinin aktif/pasif kontrolü
        public string MessageUploadFile { get; set; } //documents/users/userfiles/kanlaboratuvarsonuc.xls
        public virtual PatientUser PatientOwner { get; set; } 
        public virtual DoctorUser DoctorOwner { get; set; }
    }
}
