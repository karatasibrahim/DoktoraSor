using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoktoraSor.Entities
{ 
    [Table("Sales")]
   public class Sales:MyEntityBase
    {    [DisplayName("Ödeme")]
         public bool IsPay { get; set; } //ödeme kontrolü
        [DisplayName("Ödeme Tutarı")]
        public int PayCount { get; set; } //ödeme miktarı
        [DisplayName("Bakiye")]
        public int AccountBalance { get; set; } //bakiye miktarı
        [DisplayName("Mesaj Hakkı")]
        public int MessageAndQuestionCount { get; set; } //satın alınan bakiyeye göre oluşacak olan mesaj ya da soru yazma hakkı miktarı
        public virtual Product Product { get; set; }
        public virtual DoctorUser DoctorUser { get; set; }
        public virtual PatientUser PatientUser { get; set; }
    }
}
