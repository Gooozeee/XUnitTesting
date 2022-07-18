using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingApp.Functionality;

namespace TestingApp.Test
{
    public class UserManagementTesting
    {
        [Fact]
        public void Add_CreateUser()
        {
            // Arrange
            var userManagement = new UserManagement();

            // Act
            userManagement.Add(new("Michal", "Guzy"));

            // Assert
            var savedUser = Assert.Single(userManagement.AllUsers);
            Assert.NotNull(savedUser);
            Assert.Equal("Michal", savedUser.FirstName);
            Assert.Equal("Guzy", savedUser.LastName);
            Assert.False(savedUser.VerifiedEmail);
        }

        [Fact]
        public void Update_UpdateMobileNumber()
        {
            // Arrange
            var userManagement = new UserManagement();

            // Act
            userManagement.Add(new("Michal", "Guzy"));

            var firstUser = userManagement.AllUsers.First();
            firstUser.Phone = "+440000000000";

            userManagement.UpdatePhone(firstUser);

            // Assert
            var savedUser = Assert.Single(userManagement.AllUsers);
            Assert.NotNull(savedUser);
            Assert.Equal("+440000000000", savedUser.Phone); 
        }
    }
}
