using Microsoft.AspNetCore.Mvc;
using Moq;
using MyApplication.Application.Features.CategoryFeature.DTO;
using MyApplication.Application.Features.CategoryFeature.Services;
using MyApplication.WebApi.Controllers;

namespace MyApplication.WebApi.Tests.ControllersTests;

public class CategoryControllerTests
{
    private readonly Mock<ICategoryService> _categoryServiceMock;
    private readonly CategoryController _controller;

    public CategoryControllerTests()
    {
        _categoryServiceMock = new Mock<ICategoryService>();
        _controller = new CategoryController(_categoryServiceMock.Object);
    }

    [Fact]
    public async Task GetAll_ShouldReturnOkResult_WithCategories()
    {
        // Arrange
        var categories = new List<CategoryResponse>
        {
            new()
            {
                Id = 1,
                Name = "Electronics",
                Slug = "electronics",
                CreatedAt = DateTime.UtcNow
            }
        };

        _categoryServiceMock
            .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(categories);

        // Act
        var result = await _controller.GetAll(CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var value = Assert.IsAssignableFrom<IEnumerable<CategoryResponse>>(okResult.Value);

        Assert.Single(value);
    }

    [Fact]
    public async Task GetById_WhenCategoryExists_ShouldReturnOkResult()
    {
        // Arrange
        var category = new CategoryResponse
        {
            Id = 1,
            Name = "Electronics",
            Slug = "electronics",
            CreatedAt = DateTime.UtcNow
        };

        _categoryServiceMock
            .Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);

        // Act
        var result = await _controller.GetById(1, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var value = Assert.IsType<CategoryResponse>(okResult.Value);

        Assert.Equal(1, value.Id);
    }

    [Fact]
    public async Task GetById_WhenCategoryDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        _categoryServiceMock
            .Setup(x => x.GetByIdAsync(99, It.IsAny<CancellationToken>()))
            .ReturnsAsync((CategoryResponse?)null);

        // Act
        var result = await _controller.GetById(99, CancellationToken.None);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Create_ShouldReturnCreatedAtAction()
    {
        // Arrange
        var request = new CreateCategoryRequest
        {
            Name = "Books",
            Slug = "books"
        };

        var response = new CategoryResponse
        {
            Id = 1,
            Name = "Books",
            Slug = "books",
            CreatedAt = DateTime.UtcNow
        };

        _categoryServiceMock
            .Setup(x => x.CreateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _controller.Create(request, CancellationToken.None);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);

        Assert.Equal(nameof(CategoryController.GetById), createdResult.ActionName);

        var value = Assert.IsType<CategoryResponse>(createdResult.Value);
        Assert.Equal(1, value.Id);
    }

    [Fact]
    public async Task Delete_WhenCategoryExists_ShouldReturnNoContent()
    {
        // Arrange
        _categoryServiceMock
            .Setup(x => x.DeleteAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(1, CancellationToken.None);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_WhenCategoryDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        _categoryServiceMock
            .Setup(x => x.DeleteAsync(99, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(99, CancellationToken.None);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}