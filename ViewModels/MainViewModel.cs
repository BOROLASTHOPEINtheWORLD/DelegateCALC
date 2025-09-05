using DelegateCALC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateCALC.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public CalculatorViewModel CalculatorViewModel { get; } = new CalculatorViewModel();
    }
}
