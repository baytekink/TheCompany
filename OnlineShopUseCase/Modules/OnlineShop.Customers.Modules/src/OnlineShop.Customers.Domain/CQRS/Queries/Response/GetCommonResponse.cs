using OnlineShop.Customers.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Customers.Domain.Queries.Response
{
    public abstract class GetCommonResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public DateTime BirthDate { get; set; }
        public CustomerGender Gender { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public byte IsDeleted { get; set; }
    }
}
