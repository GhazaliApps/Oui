using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuiHop.Interfaces
{
    public interface IOperationSubject
    {
        void Subscribe(IOperationObserver observer);
        void UnSubscribe(IOperationObserver observer);
        void Notify(string key);
    }
}
