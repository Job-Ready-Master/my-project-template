using Microsoft.AspNetCore.Mvc;
using Moq;
using MyApplication.Application.Features.ProductFeature.DTO;
using MyApplication.Application.Features.ProductFeature.Services;
using MyApplication.WebApi.Controllers;

namespace MyApplication.WebApi.Tests.ControllersTests;

public class ProductControllerTests
{
    private readonly Mock<IProductService> _productServiceMock;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _productServiceMock = new Mock<IProductService>();
        _controller = new ProductController(_productServiceMock.Object);
    }

    [Fact]
    public async Task GetAll_ShouldReturnOkResult_WithProducts()
    {
        // Arrange
        var products = new List<ProductResponse>
        {
            new()
            {
                Id = 1,
                Name = "Laptop",
                Price = 1500,
                CategoryId = 1,
                CategoryName = "Electronics"
            }
        };

        _productServiceMock
            .Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(products);

        // Act
        var result = await _controller.GetAll(CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var value = Assert.IsAssignableFrom<IEnumerable<ProductResponse>>(okResult.Value);

        Assert.Single(value);
        Assert.Equal("Laptop", value.First().Name);
    }

    [Fact]
    public async Task GetById_WhenProductExists_ShouldReturnOkResult()
    {
        // Arrange
        var product = new ProductResponse
        {
            Id = 1,
            Name = "Laptop",
            Price = 1500,
            CategoryId = 1,
            CategoryName = "Electronics"
        };

        _productServiceMock
            .Setup(s => s.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        // Act
        var result = await _controller.GetById(1, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var value = Assert.IsType<ProductResponse>(okResult.Value);

        Assert.Equal(product.Id, value.Id);
        Assert.Equal(product.Name, value.Name);
    }

    [Fact]
    public async Task GetById_WhenProductDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        _productServiceMock
            .Setup(s => s.GetByIdAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync((ProductResponse?)null);

        // Act
        var result = await _controller.GetById(999, CancellationToken.None);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Create_WhenCategoryExists_ShouldReturnCreatedAtAction()
    {
        // Arrange
        var request = new CreateProductRequest
        {
            Name = "Laptop",
            Price = 1500,
            CategoryId = 1
        };

        var response = new ProductResponse
        {
            Id = 1,
            Name = "Laptop",
            Price = 1500,
            CategoryId = 1,
            CategoryName = "Electronics"
        };

        _productServiceMock
            .Setup(s => s.CreateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _controller.Create(request, CancellationToken.None);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);

        Assert.Equal(nameof(ProductController.GetById), createdResult.ActionName);

        var value = Assert.IsType<ProductResponse>(createdResult.Value);

        Assert.Equal(response.Id, value.Id);
        Assert.Equal(response.Name, value.Name);
    }

    [Fact]
    public async Task Create_WhenCategoryDoesNotExist_ShouldReturnBadRequest()
    {
        // Arrange
        var request = new CreateProductRequest
        {
            Name = "Laptop",
            Price = 1500,
            CategoryId = 99
        };

        _productServiceMock
            .Setup(s => s.CreateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync((ProductResponse?)null);

        // Act
        var result = await _controller.Create(request, CancellationToken.None);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);

        Assert.Equal(
            $"Category {request.CategoryId} does not exist.",
            badRequest.Value);
    }

    [Fact]
    public async Task Delete_WhenProductExists_ShouldReturnNoContent()
    {
        // Arrange
        _productServiceMock
            .Setup(s => s.DeleteAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(1, CancellationToken.None);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_WhenProductDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        _productServiceMock
            .Setup(s => s.DeleteAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(999, CancellationToken.None);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}