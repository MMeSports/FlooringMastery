using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Data;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models;

namespace FlooringMastery.BLL
{
    public class ProductManager : IProductRepository
    {
        private IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public Product GetProductInfo(string product)
        {
            return _productRepository.GetProductInfo(product);
        }

        public bool ProductExistsInFile(string product)
        {
            return _productRepository.ProductExistsInFile(product);
        }
    }
}
