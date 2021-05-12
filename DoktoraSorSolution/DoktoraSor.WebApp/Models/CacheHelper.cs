using DoktoraSor.BusinessLayer;
using DoktoraSor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace DoktoraSor.WebApp.Models
{
    public class CacheHelper
    {
        public static List<Category> GetCategoriesFromCache()
        {

            var result = WebCache.Get("category-cache");
            if (result==null)
            {
                CategoryManager categoryManager = new CategoryManager();
                result = categoryManager.List();
                WebCache.Set("category-cache",  categoryManager.List(),20, true); //20 dk bir cache i yeniler

            }
            return result;
        }

        public static void RemoveCategoriesFromCache()
        {
            Remove("category-cache"); //kategori cache sil
        }
        public static void Remove(string key)
        {
            WebCache.Remove(key);
        }
    }
}