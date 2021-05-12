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
    [Table("Products")]
     public class Product
    {   [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Paket Adı"), Required, StringLength(50)]
        public string Name { get; set; } //ürün adı
        [DisplayName("Açıklama"),Required, StringLength(100)]
        public string Description { get; set; } //ürün açıklama
        [DisplayName("Ürün Fiyatı"),Required]
        public int Price { get; set; } //ürün fiyatı
        [DisplayName("Kupon Kodu")]
        public string CouponCode { get; set; } //kupon kodu
        [DisplayName("Kupon Kullanım Durumu")]
        public bool IsCouponCode { get; set; } //kupon kodu uygulanmış mı
        public List<Sales>Sales { get; set; }

    }
}
