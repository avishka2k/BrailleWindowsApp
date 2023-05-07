using BrailleApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrailleApi.Tests.Controllers
{
    public class DotPatternControllerTests
    {
        [Fact]
        public void Rectangle_ReturnsOkResult()
        {
            // Arrange
            var controller = new DotPatternController();

            // Act
            var result = controller.Rectangle(3, 2);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Rectangle_ReturnsCorrectPattern()
        {
            // Arrange
            var controller = new DotPatternController();

            // Act
            var result = controller.Rectangle(3, 2) as OkObjectResult;
            var dotPattern = result.Value as string;

            // Assert
            Assert.Equal("...\n...\n", dotPattern);
        }
    }
}
