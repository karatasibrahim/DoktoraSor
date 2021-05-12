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
    [Table("Comments")]
    public  class Comment:MyEntityBase
    {
        [DisplayName("Yorum"),Required,StringLength(300, ErrorMessage ="{0} alanı max. {1} karakter olmalıdır.")]
        public string Text { get; set; } //yorum
        public bool IsApproval { get; set; } // yorum kontrolü
        public virtual Note Note { get; set; }
        public virtual PatientUser PatientOwner { get; set; }
        public virtual DoctorUser DoctorOwner { get; set; }
    }
}
