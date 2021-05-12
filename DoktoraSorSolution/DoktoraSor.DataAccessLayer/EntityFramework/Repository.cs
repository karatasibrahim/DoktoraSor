using DoktoraSor.Common;
using DoktoraSor.Core.DataAccess;
using DoktoraSor.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DoktoraSor.DataAccessLayer.EntityFramework
{
   public class Repository<T>:RepositoryBase, IDataAccess<T> where T: class // generic olan t tipi class olmak zorundadır.
    {
        // private DatabaseContext db = new DatabaseContext(); her proje çalıştığında db yenilenmeyecek. Bunun için RepositoryBase class ını kullandık.
        
        private DbSet<T> _objectSet;

        public Repository()
        {
            
            _objectSet = context.Set<T>();
        }
       public List<T> List() 
        {
           return _objectSet.ToList();
        }
        public IQueryable<T> ListIQueryable() 
        {
            return _objectSet.AsQueryable<T>();
        }
        public List<T> List(Expression<Func<T,bool>> where) // istenilen özelliğe göre arama yapma
        {
            return _objectSet.Where(where).ToList();
        }
        public int Insert(T obj) //Insert edilecek obj T den gelen class nesnesine Insert edilir.
        {
            _objectSet.Add(obj);
            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;
                DateTime now = DateTime.Now;
                o.CreatedOn = now;
                o.ModifiedOn = now;
                o.ModifiedUserName = App.Common.GetCurrentUsername();  
            }
            return Save();
        }
        public int Update(T obj)
        {
            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;
                o.ModifiedOn = DateTime.Now;
                o.ModifiedUserName = App.Common.GetCurrentUsername();  
            }
            return Save();
        }
        public int Delete(T obj)
        {
            //if (obj is MyEntityBase)
            //{
            //    MyEntityBase o = obj as MyEntityBase;
            //    o.ModifiedOn = DateTime.Now;
            //    o.ModifiedUserName = "system"; //işlem yapan kullanıcı adı yazılacak
            //}
            _objectSet.Remove(obj);

            return Save();
        }
        public int Save() //save metodu int döner
        {
            return context.SaveChanges();
        }
        public T Find(Expression<Func<T, bool>> where) // tek bir kayıt arama
        {
            return _objectSet.FirstOrDefault(where);
        }
    }
}
