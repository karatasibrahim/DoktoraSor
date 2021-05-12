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
    [Table("Expertises")]
     public class Expertise
    {   [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Uzmanlık"),Required, StringLength(50, ErrorMessage ="{0} alanı max. {1} karakter olmalıdır.")]
        public string Title { get; set; }
        public virtual Department Department { get; set; }
    }
}
