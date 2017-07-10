using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
namespace FlooringMastery.Models.Interfaces
{
    public interface ITaxRepository
    {
        List<State> GetAllStates();
        State GetStateTaxInfo(string state);
        bool StateExistsInFile(string state);
    }
}
