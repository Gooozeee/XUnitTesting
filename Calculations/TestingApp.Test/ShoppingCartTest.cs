using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingApp.Functionality;

namespace TestingApp.Test
{
    public class DBServiceMock : IDbService
    {
        public bool ProcessResult { get; set; }
        public Product ProductBeingProcessed { get; set; }
        public int ProductIdBeingProcessed { get; set; }

        public bool RemoveItemFromShoppingCart(int? prodId)
        {
            if(prodId == null)
            {
                return false;
                
            }
            
            ProductIdBeingProcessed = Convert.ToInt32(prodId);
            return true;
        }

        public bool SaveItemToShoppingCart(Product? prod)
        {
            if(prod == null)
            {
                return false;
            }

            ProductBeingProcessed = prod;
            return true;
        }
    }

    public class ShoppingCartTest
    {
        [Fact]
        public void AddProduct_Success()
        {
            // Given
            var dbMock = new DBServiceMock();
            dbMock.ProcessResult = true;
            var shoppingCart = new ShoppingCart(dbMock);

            // When
            var product = new Product(1, "shoes", 150);
            var result = shoppingCart.AddProduct(product);

            // Then
            Assert.True(result);
            Assert.Equal(result, dbMock.ProcessResult);
            Assert.Equal("shoes", dbMock.ProductBeingProcessed.Name);
        }

        [Fact]
        public void AddProduct_Failure_DueToInvalidPayload()
        {
            // Given
            var dbMock = new DBServiceMock();
            dbMock.ProcessResult = false;
            var shoppingCart = new ShoppingCart(dbMock);

            // When
            var result = shoppingCart.AddProduct(null);

            // Then
            Assert.False(result);
            Assert.Equal(result, dbMock.ProcessResult);
        }

        [Fact]
        public void RemoveProduct_Success()
        {
            // Given
            var dbMock = new DBServiceMock();
            dbMock.ProcessResult = true;

            var shoppingCart = new ShoppingCart(dbMock);

            // When
            var product = new Product(1, "shoes", 150);
            var result = shoppingCart.AddProduct(product);

            var deleteResult = shoppingCart.DeleteProduct(product.Id);

            // Then
            Assert.True(deleteResult);
            Assert.Equal(deleteResult, dbMock.ProcessResult);
        }
    }
}
