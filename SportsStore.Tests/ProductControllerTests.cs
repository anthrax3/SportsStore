using System.Collections.Generic;
using System.Linq;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{


  public class ProductControllerTests
  {

    [Fact]
    public void Can_Paginate()
    {
      // Arrange
      Mock<IProductRepository> mock = new Mock<IProductRepository>();
      mock.Setup(m => m.Products).Returns(new Product[] {

      });
    }
  }
}
