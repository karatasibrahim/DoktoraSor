using DoktoraSor.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoktoraSor.BusinessLayer.Results
{ //hata mesajları için oluşturuldu
   public class BusinessLayerResult<T> where T:class
    {
        public List<ErrorMessageObj> Errors { get; set; } //enum dan gelen hata kodları
        public T Result { get; set; }
        public BusinessLayerResult() //Errors listesine ekleme yapmak için yazıldı
        {
            Errors = new List<ErrorMessageObj>();
            
        }
        public void AddError(ErrorMessageCode code, string message) //Dönen hataların kod ve mesaj bilgileri
        {
            Errors.Add(new ErrorMessageObj() { Code = code, Message = message });
        }
    }
}
