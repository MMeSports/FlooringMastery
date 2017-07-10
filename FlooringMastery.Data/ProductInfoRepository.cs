using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using System.IO;
using FlooringMastery.Models.Responses;
using System.Text.RegularExpressions;

namespace FlooringMastery.Data
{
    public class ProductInfoRepository : IProductRepository
    {

        private static string _path = @"C:\Users\praisekek\Desktop\OrderData\Products.txt";
        public List<Product> GetAllProducts()
        {
            List<Product> productInfo = new List<Product>();
            List<string> allProductsFromFile = File.ReadAllLines(_path).ToList();

            foreach (var product in allProductsFromFile.Skip(1))
            {
                string[] columns = product.Split(',');

                Product p = new Product();

                p.ProductType = columns[0];
                p.MaterialCostPerSquareFoot = decimal.Parse(columns[1]);
                p.LaborCostPerSquareFoot = decimal.Parse(columns[2]);

                productInfo.Add(p);
            }
            return productInfo;
        }

        public Product GetProductInfo(string product)
        {
            var allProducts = GetAllProducts();
            var selectedProduct = allProducts.FirstOrDefault(p => p.ProductType.ToUpper() == product.ToUpper());
            return selectedProduct;
        }

        public bool ProductExistsInFile(string product)
        {
            var productInfo = GetAllProducts();
            if (productInfo.Any(p => p.ProductType == product))
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
