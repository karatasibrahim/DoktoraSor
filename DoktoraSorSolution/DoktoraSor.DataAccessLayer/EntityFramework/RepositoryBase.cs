using DoktoraSor.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoktoraSor.DataAccessLayer.EntityFramework
{
   public class RepositoryBase
    {
        protected static DatabaseContext  context;
        private static object _lockSync = new object(); //lock için üretilen obje

        protected RepositoryBase() // bu class new lenemez
        {
         CreateContext();
        }
        private static void CreateContext() //static olmasınınn sebebi yenilenmesini istememiz
        {
            if (context==null)
            {
                lock (_lockSync) //multiThread işlemleri için nesne lock a alınır. Biri bir işi bitirmeden diğeri o işi yapamaz.
                {
                    if (context==null)
                    {
                        context = new DatabaseContext();
                    }                    
                }                
            }       
        }
    }
}
