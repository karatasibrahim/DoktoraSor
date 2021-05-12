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
    [Table("AppComments")]
   public class AppComment
    { 
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Yorum"),Required, StringLength(300, ErrorMessage ="{0} alanı max. {1} karakter olmalıdır.")]
        public string Text { get; set; } //yorum
        [DisplayName("Yayınlanma Durumu")]
        public bool IsApproval { get; set; } // yorum kontrolü         
        public virtual PatientUser PatientOwner { get; set; }
    }
}
