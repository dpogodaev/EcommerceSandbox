using AutoMapper;
using EcommerceSandbox.App.Domain;
using EcommerceSandbox.App.Services.Dtos;
using EcommerceSandbox.App.Services.Models.Product;
using EcommerceSandbox.App.Services.Services;
using NUnit.Framework;

namespace EcommerceSandbox.AppServices.Tests.Services.Mapping;

public class ProductAppServiceMappingTests
{
    private MapperConfiguration _mappingConfig;

        [SetUp]
        public void Setup()
        {
            _mappingConfig = new MapperConfiguration(cfg => cfg.AddProfile<ProductAppService.Mapping>());
        }

        [Test]
        public void Mapper_Configuration_IsValid()
        {
            Assert.DoesNotThrow(() => _mappingConfig?.AssertConfigurationIsValid());
        }

        [Test]
        public void Mapper_FromCreationModelToDomain_IsValid()
        {
            // Arrange
            var creationModel = new ProductCreationModel
            {
                Name = "Name",
                Category = "Category",
                PurchasePrice = 10,
                RetailPrice = 20
            };

            // Act
            var domain = GetMapper().Map<Product>(creationModel);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(domain.Id, Is.EqualTo(0));
                Assert.That(domain.Name, Is.EqualTo(creationModel.Name));
                Assert.That(domain.Category, Is.EqualTo(creationModel.Category));
                Assert.That(domain.PurchasePrice, Is.EqualTo(creationModel.PurchasePrice));
                Assert.That(domain.RetailPrice, Is.EqualTo(creationModel.RetailPrice));
            });
        }

        [Test]
        public void Mapper_FromUpdateModelToDomain_IsValid()
        {
            // Arrange
            var updateModel = new ProductUpdateModel
            {
                Id = 1,
                Name = "Name",
                Category = "Category",
                PurchasePrice = 10,
                RetailPrice = 20
            };

            // Act
            var domain = GetMapper().Map<Product>(updateModel);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(domain.Id, Is.EqualTo(updateModel.Id));
                Assert.That(domain.Name, Is.EqualTo(updateModel.Name));
                Assert.That(domain.Category, Is.EqualTo(updateModel.Category));
                Assert.That(domain.PurchasePrice, Is.EqualTo(updateModel.PurchasePrice));
                Assert.That(domain.RetailPrice, Is.EqualTo(updateModel.RetailPrice));
            });
        }

        [Test]
        public void Mapper_FromDomainToDomain_IsValid()
        {
            // Arrange
            var source = CreateProductWithUniquePropertyValues(1);
            var dest = CreateProductWithUniquePropertyValues(2);

            // Act
            GetMapper().Map(source, dest);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(source.Id, Is.EqualTo(dest.Id));
                Assert.That(source.Name, Is.EqualTo(dest.Name));
                Assert.That(source.Category, Is.EqualTo(dest.Category));
                Assert.That(source.PurchasePrice, Is.EqualTo(dest.PurchasePrice));
                Assert.That(source.RetailPrice, Is.EqualTo(dest.RetailPrice));
            });
        }

        [Test]
        public void Mapper_FromDomainToDto_IsValid()
        {
            // Arrange
            var domain = CreateProductWithUniquePropertyValues();

            // Act
            var dto = GetMapper().Map<ProductDto>(domain);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto.Id, Is.EqualTo(domain.Id));
                Assert.That(dto.Name, Is.EqualTo(domain.Name));
                Assert.That(dto.Category, Is.EqualTo(domain.Category));
                Assert.That(dto.PurchasePrice, Is.EqualTo(domain.PurchasePrice));
                Assert.That(dto.RetailPrice, Is.EqualTo(domain.RetailPrice));
            });
        }

        private IMapper GetMapper()
        {
            return _mappingConfig?.CreateMapper();
        }

        private static Product CreateProductWithUniquePropertyValues(int index = 0)
        {
            var valueGenerator = new ValueGenerator(index);

            return new Product
            {
                Id = valueGenerator.GetValue<int>(),
                Name = valueGenerator.GetValue<string>(),
                Category = valueGenerator.GetValue<string>(),
                PurchasePrice = valueGenerator.GetValue<decimal>(),
                RetailPrice = valueGenerator.GetValue<decimal>(),
            };
        }

        private class ValueGenerator
        {
            private int _currentValue;

            public ValueGenerator(int index)
            {
                _currentValue = index * 100;
            }

            public T GetValue<T>()
            {
                var newValue = (T)Convert.ChangeType(_currentValue, typeof(T));
                _currentValue++;
                return newValue;
            }
        }
}