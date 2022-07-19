using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingApp.Functionality
{
    public record Product(int Id, String Name, double Price);

    public interface IDbService
    {
        bool SaveItemToShoppingCart(Product? prod);
        bool RemoveItemFromShoppingCart(int? prodId);
    }

    public class ShoppingCart
    {
        private IDbService _dbService;

        public ShoppingCart(IDbService dbService)
        {
            _dbService = dbService;
        }

        public bool AddProduct(Product? product)
        {
            if(product == null)
            {
                return false;
            }

            if(product.Id == 0)
            {
                return false;
            }

            _dbService.SaveItemToShoppingCart(product);
            return true;
        }

        public bool DeleteProduct(int? id)
        {
            if(id == null)
            {
                return false;
            }

            if(id == 0)
            {
                return false;
            }

            _dbService.RemoveItemFromShoppingCart(Convert.ToInt32(id));
            return true;
        }
    }
}
