using Aranda.CatalogProductCore.Business.Interface;
using Aranda.CatalogProductCore.Repository.Dto;
using Aranda.CatalogProductCore.Test.MockDbContext;
using CatalogProductAranda.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Aranda.CatalogProductCore.Test.ControllerTest
{
    public class CatalogProductControllerTest
    {
        private CatalogProductController _currentController;
        private readonly Mock<ICatalogProductService> _catalogProductService = new();
        //private Fixture _autodata;
        public CatalogProductControllerTest()
        {
            _currentController = new CatalogProductController(
                _catalogProductService.Object);
            //_autodata = new Fixture();
        }

        [Theory]
        [InlineData(1, 200)]
        [InlineData(2, 400)]
        public async Task CreateProductTest(int index,int expected) {

            //Arrange
            switch (index)
            {
                case 1:
                    _catalogProductService
                        .Setup(t => t.AddProduct(It.IsAny<ProductBaseRequest>()))
                        .Returns(Task.FromResult(true));
                    break;
                case 2:
                    _catalogProductService
                        .Setup(t => t.AddProduct(It.IsAny<ProductBaseRequest>()))
                        .Throws(new Exception("Error"));
                    break;
            }
            //Act
            IActionResult response = await _currentController.AddProduct(It.IsAny<ProductBaseRequest>());
            var result = response as ObjectResult;

            //Assert
            Assert.Equal(result.StatusCode, expected);
        }

        [Theory]
        [InlineData(1, 200)]
        [InlineData(2, 400)]
        [InlineData(3, 404)]
        public async Task UpdateProductTest(int index, int expected)
        {

            //Arrange
            switch (index)
            {
                case 1:
                    _catalogProductService
                        .Setup(t => t.UpdateProduct(It.IsAny<ProductModifyRequest>()))
                        .Returns(Task.FromResult(true));
                    break;
                case 2:
                    _catalogProductService
                        .Setup(t => t.UpdateProduct(It.IsAny<ProductModifyRequest>()))
                        .Throws(new Exception("Error"));
                    break;
                case 3:
                    _catalogProductService
                        .Setup(t => t.UpdateProduct(It.IsAny<ProductModifyRequest>()))
                        .Returns(Task.FromResult(false));
                    break;
            }
            //Act
            IActionResult response = await _currentController.UpdateProduct(It.IsAny<ProductModifyRequest>());
            var result = response as ObjectResult;

            //Assert
            Assert.Equal(result.StatusCode, expected);
        }

        [Theory]
        [InlineData(1, 200)]
        [InlineData(2, 400)]
        [InlineData(3, 404)]
        public async Task DeleteProductTest(int index, int expected)
        {

            //Arrange
            switch (index)
            {
                case 1:
                    _catalogProductService
                        .Setup(t => t.DeleteProduct(It.IsAny<int>()))
                        .Returns(Task.FromResult(true));
                    break;
                case 2:
                    _catalogProductService
                        .Setup(t => t.DeleteProduct(It.IsAny<int>()))
                        .Throws(new Exception("Error"));
                    break;
                case 3:
                    _catalogProductService
                        .Setup(t => t.DeleteProduct(It.IsAny<int>()))
                        .Returns(Task.FromResult(false));
                    break;
            }
            //Act
            IActionResult response = await _currentController.DeleteProduct(It.IsAny<int>());
            var result = response as ObjectResult;

            //Assert
            Assert.Equal(result.StatusCode, expected);
        }

        [Theory]
        [InlineData(1, 200)]
        [InlineData(2, 400)]
        public async Task GetAllProductsTest(int index, int expected)
        {

            //Arrange
            switch (index)
            {
                case 1:
                    _catalogProductService
                        .Setup(t => t.GetAllProducts(It.IsAny<TableViewRequest>()))
                        .Returns(Task.FromResult(It.IsAny<ProductListResponse>()));
                    break;
                case 2:
                    _catalogProductService
                        .Setup(t => t.GetAllProducts(It.IsAny<TableViewRequest>()))
                        .Throws(new Exception("Error"));
                    break;
            }
            //Act
            IActionResult response = await _currentController.GetAllProducts(It.IsAny<TableViewRequest>());
            var result = response as ObjectResult;

            //Assert
            Assert.Equal(result.StatusCode, expected);
        }
    }
}