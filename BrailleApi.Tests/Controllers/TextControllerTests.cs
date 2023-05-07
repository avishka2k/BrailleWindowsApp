using System;
using BrailleApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BrailleApi.Tests.Controllers
{
    public class TextControllerTests
    {
        private readonly TextController _controller;

        public TextControllerTests()
        {
            _controller = new TextController();
        }

        [Fact]
        public void GetBraille_ReturnsOkResult()
        {
            // Arrange
            var text = "hello world";

            // Act
            var result = _controller.GetBraille(text);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetBraille_ReturnsCorrectBraille()
        {
            // Arrange
            var text = "hello world";

            // Act
            var result = _controller.GetBraille(text) as OkObjectResult;
            var braille = result.Value as string;

            // Assert
            Assert.Equal("⠓⠑⠇⠇⠕ ⠺⠕⠗⠇⠙", braille);
        }

        [Fact]
        public void GetBraille_ReturnsEmptyBrailleForEmptyText()
        {
            // Arrange
            var text = "";

            // Act
            var result = _controller.GetBraille(text) as OkObjectResult;
            var braille = result.Value as string;

            // Assert
            Assert.Equal("", braille);
        }
    }
}
