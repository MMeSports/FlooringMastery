using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.BLL
{
    public class TaxManager : ITaxRepository
    {
        private ITaxRepository _taxrepository;

        public TaxManager(ITaxRepository taxrepository)
        {
            _taxrepository = taxrepository;
        }

        public List<State> GetAllStates()
        {
            return _taxrepository.GetAllStates();
        }

        public bool StateExistsInFile(string state)
        {
            return _taxrepository.StateExistsInFile(state);
        }
        public State GetStateTaxInfo(string state)
        {
            return _taxrepository.GetStateTaxInfo(state);
        }
    }
}
