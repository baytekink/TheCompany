using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCompany.Domain.Shared.Common.QueueMessaging
{
    //format [Consumer]_[Event/MessageName]Queue
    public interface IProducer<in T>
    {
        Task SendAsync(T producedObj);
    }
}
