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
    [Table("Categories")]
   public class Category:MyEntityBase
    {
        [DisplayName("Kategori"),Required, StringLength(50, ErrorMessage ="{0} alanı max. {1} karakter olmalıdır.")]
        public string Title { get; set; } //başlık
        [DisplayName("Açıklama"),StringLength(150, ErrorMessage ="{0} alanı max. {1} karakter olmalıdır.")]
        public string Description { get; set; } //açıklama

        public virtual List<Note> Notes { get; set; } //bir kategorinin birden çok notu vardır
        
        public Category()  // categori yenilendiği zaman otomatik notların eklenmesi için uygulanacak
        {
            Notes = new List<Note>();
        }
    }
}
