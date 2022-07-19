using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingApp.Functionality;

namespace TestingApp.Test
{
    // ------------------------------ \\
    // Manually created DBServiceMock \\
    // ------------------------------ \\

    //public class DBServiceMock : IDbService
    //{
    //    public bool ProcessResult { get; set; }
    //    public Product ProductBeingProcessed { get; set; }
    //    public int ProductIdBeingProcessed { get; set; }

    //    public bool RemoveItemFromShoppingCart(int? prodId)
    //    {
    //        if(prodId == null)
    //        {
    //            return false;

    //        }

    //        ProductIdBeingProcessed = Convert.ToInt32(prodId);
    //        return true;
    //    }

    //    public bool SaveItemToShoppingCart(Product? prod)
    //    {
    //        if(prod == null)
    //        {
    //            return false;
    //        }

    //        ProductBeingProcessed = prod;
    //        return true;
    //    }
    //}

    public class ShoppingCartTest
    {
        public readonly Mock<IDbService> _dbServiceMock = new();

        [Fact]
        public void AddProduct_Success()
        {
            // Given
            var shoppingCart = new ShoppingCart(_dbServiceMock.Object);

            // When
            var product = new Product(1, "shoes", 150);
            var result = shoppingCart.AddProduct(product);

            // Then
            Assert.True(result);
            _dbServiceMock.Verify(x => x.SaveItemToShoppingCart(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void AddProduct_Failure_DueToInvalidPayload()
        {
            // Given
            var shoppingCart = new ShoppingCart(_dbServiceMock.Object);

            // When
            var result = shoppingCart.AddProduct(null);

            // Then
            Assert.False(result);
            _dbServiceMock.Verify(x => x.SaveItemToShoppingCart(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public void RemoveProduct_Success()
        {
            // Given
            var shoppingCart = new ShoppingCart(_dbServiceMock.Object);

            // When
            var product = new Product(1, "shoes", 150);
            var result = shoppingCart.AddProduct(product);

            var deleteResult = shoppingCart.DeleteProduct(product.Id);

            // Then
            Assert.True(deleteResult);
            _dbServiceMock.Verify(x => x.SaveItemToShoppingCart(It.IsAny<Product>()), Times.Once);
        }
    }
}
