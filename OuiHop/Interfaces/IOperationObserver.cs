using OuiHop.Controllers.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuiHop.Interfaces
{
    public interface IOperationObserver
    {
        void Update(BaseOperation operation);
    }
}
