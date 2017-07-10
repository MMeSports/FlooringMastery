using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Interfaces
{
    public interface IProductRepository
    {
        Product GetProductInfo(string product);
        bool ProductExistsInFile(string product);
        List<Product> GetAllProducts();
    }
}
