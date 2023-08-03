using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Repository
{
    public class CategoryRepository : BaseRepository, IRepository<Category>
    {
        private static CategoryRepository _instance = new CategoryRepository();
        public static CategoryRepository GetRepository()
        {
            return _instance;
        }

        public IQueryable<Category> GetAll()
        {
            var context = new BasiliskTFContext();
            return context.Categories;
        }

        public Category GetSingle(object id)
        {
            var context = new BasiliskTFContext();
            
            var category = context.Categories.SingleOrDefault(a => a.Id == (long)id);
            return category;
            
        }

       
        public bool Insert(Category model)
        {
            try
            {
                using(var context = new BasiliskTFContext())
                {
                    context.Categories.Add(model);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        
        }

        public bool Update(Category model)
        {
            try
            {
                using(var context = new BasiliskTFContext())
                {
                    var oldCat = context.Categories.SingleOrDefault(a=> a.Id == model.Id);
                    MappingModel(oldCat, model);

                    context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(object id)
        {
            try
            {
                using(var context = new BasiliskTFContext())
                {
                    var category = context.Categories.SingleOrDefault(a => a.Id == (long)id);
                    if(category == null)
                    {
                        return false;
                    }

                    var products = context.Products.Any(a => a.CategoryId == (long)id);
                    if (products)
                    {
                        return false;
                    }
                    else
                    {
                        context.Remove(category);
                        context.SaveChanges();
                    }

                }

                return true;
            }
            catch
            {
                return false;
            }
        
        }

        public bool UpdateDescription(long id, string desc)
        {
            using(var context = new BasiliskTFContext())
            {
                var category = context.Categories.SingleOrDefault(c => c.Id == id);
                category.Description = desc;
                context.SaveChanges();
                return true;
            }
        }

        





    }
}
