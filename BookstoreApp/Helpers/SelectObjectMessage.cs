using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Helpers
{
    public class SelectedObjectMessage<SelectedObjectType>
    {
        public object WhoRequestedToSelect { get; set; }
        public SelectedObjectType SelectedObject { get; set; }

        public SelectedObjectMessage(object whoRequestedToSelect, SelectedObjectType selectedObject)
        {
            WhoRequestedToSelect = whoRequestedToSelect;
            SelectedObject = selectedObject;
        }

    }
}
