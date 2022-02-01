using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheCompany.Domain.Shared.Common.Helper
{
    public class DateCreatorUtc : IDateCreator
    {
        public DateTime CreateNow()
        {
            return DateTime.UtcNow;
        }
    }
}
