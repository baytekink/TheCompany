using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheCompany.Domain.Shared.Common.Helper
{
    public class IdGenerator : IIdGenerator
    {
        public Guid GenerateId()
        {
            return Guid.NewGuid();            
        }
    }
}
