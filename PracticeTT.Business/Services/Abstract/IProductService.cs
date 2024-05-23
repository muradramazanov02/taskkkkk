using PracticeTT.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTT.Business.Services.Abstract
{
    public interface IProductService
    {
        Task AddProduct(Product product);
        void DeleteProduct(int id);
        void UpdateProduct(Product newProduct, int id);
        Product GetProduct(Func<Product, bool>? func = null);
        List<Product> GetAllProducts(Func<Product, bool>? func = null);
    }
}
