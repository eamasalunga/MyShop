using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;

        List<Product> products;

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;

            if (products == null)
                products = new List<Product>();
        }

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product p)
        {
            products.Add(p);      
        }

        public void Update(Product product)
        {
            Product productUpdate = products.Find(p => p.Id == product.Id);

            if(productUpdate != null)
            {
                productUpdate = product;
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }


        public Product Find(string Id)
        {
            Product product= products.Find(p => p.Id == Id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string Id)
        {
            Product product = products.Find(p => p.Id == Id);

            if (product != null)
            {
                products.Remove(product);
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }
    }
}
