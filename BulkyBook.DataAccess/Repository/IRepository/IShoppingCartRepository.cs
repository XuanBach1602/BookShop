using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        public int IncrementCount(ShoppingCart cart, int count);
        public int DecrementCount(ShoppingCart cart, int count);
    }
    
}
