using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using FlooringMastery.Data;

namespace FlooringMastery.BLL
{
    public class ProductManagerFactory
    {
        public static ProductManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "Test":
                    return new ProductManager(new ProductInfoRepository());
                case "File":
                    return new ProductManager(new ProductInfoRepository());
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
