﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.ViewModels;

namespace BookstoreApp.Helpers
{
    public class OpenViewMessage
    {
        public WorkspaceViewModel ViewModelToBeOpened { get; set; } = default!;
        public object Sender { get; set; } = default!;
    }
}
