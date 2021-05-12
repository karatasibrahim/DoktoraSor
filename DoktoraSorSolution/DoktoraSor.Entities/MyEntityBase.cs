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
   public class MyEntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] //otomatik artan Id key
        public int Id { get; set; } 
        [DisplayName("Oluşturma Tarihi"),Required, ScaffoldColumn(false)]
        public DateTime CreatedOn { get; set; } //oluşturulduğu tarih
        [DisplayName("Değiştirme Tarihi"), Required, ScaffoldColumn(false)]
        public DateTime ModifiedOn { get; set; } //düzenlendiği tarih
        [DisplayName("Değişiklik Yapan Kullanıcı"), Required, StringLength(30), ScaffoldColumn(false)]
        public string ModifiedUserName { get; set; } //işlemi yapan kullanıcı
    }
}
