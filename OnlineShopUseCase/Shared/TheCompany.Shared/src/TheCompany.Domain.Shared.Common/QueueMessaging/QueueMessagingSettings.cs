using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCompany.Domain.Shared.Common.QueueMessaging
{
    //format [Consumer]_[Event/MessageName]Queue
    public static class QueueMessagingSettings
    {
        public const string CustomerChangedEventQueue = "customer-changed-queue";
        public const string ProductChangedEventQueue = "product-changed-queue";
    }
}
