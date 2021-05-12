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
    [Table("Departments")]
   public class Department
    { [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Bölüm Adı"),Required, StringLength(50, ErrorMessage ="{0} alanı max. {1} karakter olmalıdır.")]
        public string Title { get; set; }
       // public int ExpertiseId { get; set; } //department ile expertise ilişkisinin sağlanması
      //  public virtual DoctorUser DoctorOwner { get; set; }
        public virtual List<Expertise> Expertises { get; set; }
    }
}
