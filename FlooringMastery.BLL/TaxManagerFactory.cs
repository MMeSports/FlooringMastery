using FlooringMastery.Data;
using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.BLL
{
    public static class TaxManagerFactory
    {
        public static TaxManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "Test":
                    return new TaxManager(new TaxRepository());
                case "File":
                    return new TaxManager(new TaxRepository());
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
