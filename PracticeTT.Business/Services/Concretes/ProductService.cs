using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Query;
using PracticeTT.Business.Exceptions;
using PracticeTT.Business.Services.Abstract;
using PracticeTT.Core.Models;
using PracticeTT.Core.RepositoryAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileNotFoundException = PracticeTT.Business.Exceptions.FileNotFoundException;

namespace PracticeTT.Business.Services.Concretes
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _env;
        public ProductService(IProductRepository productRepository, IWebHostEnvironment env)
        {
            _productRepository = productRepository;
            _env = env;
        }
        public async Task AddProduct(Product product)
        {
            if (product.FileImage.ContentType != "img/jpeg" && product.FileImage.ContentType != "img/jpeg")
                throw new ImageContentException("fayl format movcud deyil");
            if (product.FileImage.Length > 2097152) throw new ImageSizeException("sekil olcusu 2 mb ola biler");
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(product.FileImage.FileName);
            string path = _env.WebRootPath + "\\uploads\\product\\" + fileName;
            using(FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                product.FileImage.CopyTo(fileStream);
            }
            product.ImageUrl = fileName;
            await _productRepository.AddAsync(product);
            await _productRepository.CommitAsync();
        }

        public void DeleteProduct(int id)
        {
            var exsitProduct =_productRepository.Get(x => x.Id == id);
            if (exsitProduct != null) throw new EntityNotFoundException("bele id yox");

            string path = _env.WebRootPath + "\\iploads\\product\\" + exsitProduct.FileImage;
            File.Delete(path);
            _productRepository.Delete(exsitProduct);
            _productRepository.Commit();
            
        }

        public List<Product> GetAllProducts(Func<Product, bool>? func = null)
        {
            return _productRepository.GetAll(func);
        }

        public Product GetProduct(Func<Product, bool>? func = null)
        {
            return _productRepository.Get(func); 
        }

        public void UpdateProduct(Product newProduct, int id)
        {
            var existProduct = _productRepository.Get(x =>x.Id == id);
            if (existProduct == null) throw new EntityNotFoundException("bele id yox");

            if (newProduct.FileImage != null)
            {
                if (newProduct.FileImage.ContentType != "img/jpeg" && newProduct.FileImage.ContentType != "img/jpeg")
                    throw new ImageContentException("fayl format movcud deyil");
                if (newProduct.FileImage.Length > 2097152) throw new ImageSizeException("sekil olcusu 2 mb ola biler");
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(newProduct.FileImage.FileName);
                string path = _env.WebRootPath + "\\uploads\\product\\" + fileName;
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    newProduct.FileImage.CopyTo(fileStream);
                }
                string oldPath = _env.WebRootPath + "\\iploads\\product\\" + existProduct.FileImage;
                File.Delete(oldPath);
                _productRepository.Delete(existProduct);
                existProduct.ImageUrl = oldPath;
            }
            existProduct.Name = newProduct.Name;
            existProduct.Price = newProduct.Price;
            _productRepository.Commit();
        }
    }
}
