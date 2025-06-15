using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookstoreApp.ViewModels
{
    /// <summary>
    /// This is class for element of side bar
    /// </summary>
    public class CommandViewModel : BaseViewModel
    {
        #region FieldAndProperties

        /// <summary>
        /// This is name of the button in sidebar
        /// </summary>
        public string DisplayName { get; set; }

        public string Color { get; set; }

        /// <summary>
        /// Every button contains Command, that opens tab
        /// </summary>
        public ICommand Command { get; set; }

        #endregion

        #region Konstruktor

        public CommandViewModel(string displayName, ICommand command, string color)
        {
            if (command == null)
                throw new ArgumentNullException("Command");
            DisplayName = displayName;
            Command = command;
            Color = color;
        }

        #endregion
    }
}
