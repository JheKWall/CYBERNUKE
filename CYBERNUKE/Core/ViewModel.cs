using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYBERNUKE.Core
{
    // Abstract ViewModel class that all ViewModels inherit from
    // Derived from ObservableObject so we have access to its "OnPropertyChanged" function
    public abstract class ViewModel : ObservableObject
    {

    }
}
