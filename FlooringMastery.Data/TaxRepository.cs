using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models;
using System.IO;

namespace FlooringMastery.Data
{
    public class TaxRepository : ITaxRepository
    {
        private static string _path = @"C:\Users\praisekek\Desktop\OrderData\Taxes.txt";

        public State GetStateTaxInfo(string state)
        {
            var taxInfo = GetAllStates();

            var requestedState = taxInfo.FirstOrDefault(s => s.StateName.ToUpper() == state.ToUpper());
            return requestedState;
            
        }

        public bool StateExistsInFile(string state)
        {
            var taxinfo = GetAllStates();
            if (taxinfo.Any(s => s.StateName.ToUpper() == state.ToUpper())){
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<State> GetAllStates()
        {
            List<State> taxInfo = new List<State>();
            List<string> allLinesFromTaxFile = File.ReadAllLines(_path).ToList();

            foreach (var line in allLinesFromTaxFile.Skip(1))
            {
                string[] columns = line.Split(',');

                State s = new State();

                s.StateAbbreviation = columns[0];
                s.StateName = columns[1];
                s.StateTax = decimal.Parse(columns[2]);

                taxInfo.Add(s);
            }
            return taxInfo;
        }
    }
}
