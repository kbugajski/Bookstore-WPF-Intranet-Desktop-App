using BookstoreApp.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookstoreApp.ViewModels
{
    // Each tab view model will extends this class
    public class WorkspaceViewModel : BaseViewModel
    {
        #region FieldsAndProperties
        //each tab has display name and close command
        public string DisplayName { get; set; } //this name of the tab

        public WorkspaceViewModel(string DisplayName)
        {
            this.DisplayName = DisplayName;
        }
        private BaseCommand _CloseCommand; //this is command to close a tab
        public ICommand CloseCommand
        {
            get
            {
                if (_CloseCommand == null)
                    _CloseCommand = new BaseCommand(() => OnRequestClose()); //this command calls method to close a tab
                return _CloseCommand;
            }
        }
        #endregion

        #region RequestClose [event]
        public event EventHandler RequestClose;
        private void OnRequestClose()
        {
            EventHandler handler = RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
        #endregion 

    }
}
