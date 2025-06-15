using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Models.Services;

namespace BookstoreApp.ViewModels
{
    public class BaseServiceViewModel<ServiceType, DtoType, ModelType>
        : WorkspaceViewModel where ServiceType : BaseService<DtoType, ModelType>, new()
        where ModelType : new()
    {
        public ServiceType Service { get; set; }
        public BaseServiceViewModel(string displayName) : base(displayName)
        {
            Service = new ServiceType();
        }
    }
}
