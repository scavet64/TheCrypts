using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TheCryptsGUI.ViewModel
{
    public abstract class ViewModelBase : ObservableProperty
    {
        public Dictionary<string, ICommand> Commands { get; protected set; }

        public ViewModelBase()
        {
            Commands = new Dictionary<string, ICommand>();
        }
    }
}
