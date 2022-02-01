using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TheCompany.Domain.Shared.Common.Repository;

namespace OnlineShop.Products.Domain.Shared.Repository
{
    public interface IProductRepository<T> : IRepositoryBase<T>
    { 
    }
}
