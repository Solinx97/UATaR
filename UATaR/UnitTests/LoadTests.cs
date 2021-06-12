using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer.Services;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    class LoadTests
    {
        private readonly IMapper _autoMapper;

        public LoadTests()
        {
            var mapConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<ExecuteLoad, ExecuteLoadDto>().ReverseMap());
            _autoMapper = mapConfiguration.CreateMapper();
        }

        [Test]
        public void CreateTestPositive()
        {
            var mockCart = new Mock<IGenericRepository<ExecuteLoadDto, int>>();
            var service = new ExecuteLoadService(mockCart.Object, _autoMapper);
            mockCart.Setup(val => val.GetAllAsync()).ReturnsAsync(new List<ExecuteLoadDto>
            {
                new ExecuteLoadDto { Id = 1,  LoadId = 1, Hours = 5 },
            });

            var executeLoad = new ExecuteLoad
            {
                Id = 3,
                LoadId = 3,
                Hours = 14
            };

            Assert.DoesNotThrowAsync(() => service.CreateAsync(executeLoad), "Create data aren't valid");
        }

        [Test]
        public void GetAllTestPositive()
        {
            var mockCart = new Mock<IGenericRepository<ExecuteLoadDto, int>>();
            mockCart.Setup(val => val.GetAllAsync()).ReturnsAsync(new List<ExecuteLoadDto>
            {
                new ExecuteLoadDto { Id = 1,  LoadId = 1, Hours = 5 },
            });

            var service = new ExecuteLoadService(mockCart.Object, _autoMapper);

            var collection = new List<ExecuteLoad>
            {
                new ExecuteLoad
                {
                    Id = 1,
                    LoadId = 1,
                    Hours = 5
                }
            };

            var actualCollection = service.GetAllAsync().GetAwaiter().GetResult();
            Assert.DoesNotThrowAsync(() => service.GetAllAsync(), "Collection isn't found");

            Assert.AreEqual(collection[0].Id, actualCollection[0].Id);
            Assert.AreEqual(collection[0].LoadId, actualCollection[0].LoadId);
            Assert.AreEqual(collection[0].Hours, actualCollection[0].Hours);
        }

        [Test]
        public void GetByIdTestPositive()
        {
            const int id = 2;
            var mockCart = new Mock<IGenericRepository<ExecuteLoadDto, int>>();
            mockCart.Setup(val => val.GetByIdAsync(id)).ReturnsAsync(new ExecuteLoadDto
            {
                Id = 2,
                LoadId = 2,
                Hours = 7
            });

            var service = new ExecuteLoadService(mockCart.Object, _autoMapper);

            var executeLoad = new ExecuteLoad
            {
                Id = 2,
                LoadId = 2,
                Hours = 7
            };

            var actualItem = service.GetByIdAsync(id).GetAwaiter().GetResult();
            Assert.DoesNotThrowAsync(() => service.GetByIdAsync(id), "Entity isn't found");

            Assert.AreEqual(executeLoad.Id, actualItem.Id);
            Assert.AreEqual(executeLoad.LoadId, actualItem.LoadId);
            Assert.AreEqual(executeLoad.Hours, actualItem.Hours);
        }

        [Test]
        public void UpdateTestPositive()
        {
            var mockCart = new Mock<IGenericRepository<ExecuteLoadDto, int>>();
            var service = new ExecuteLoadService(mockCart.Object, _autoMapper);
            mockCart.Setup(val => val.GetAllAsync()).ReturnsAsync(new List<ExecuteLoadDto>
            {
                new ExecuteLoadDto { Id = 2,  LoadId = 2, Hours = 5 },
            });

            var executeLoad = new ExecuteLoad
            {
                Id = 2,
                LoadId = 2,
                Hours = 14
            };

            Assert.DoesNotThrowAsync(() => service.UpdateAsync(executeLoad), "Update data aren't valid");
        }

        [Test]
        public void DeleteTestPositive()
        {
            var mockCart = new Mock<IGenericRepository<ExecuteLoadDto, int>>();
            var service = new ExecuteLoadService(mockCart.Object, _autoMapper);
            mockCart.Setup(val => val.GetAllAsync()).ReturnsAsync(new List<ExecuteLoadDto>
            {
                new ExecuteLoadDto { Id = 1,  LoadId = 1, Hours = 5 },
            });

            var executeLoad = new ExecuteLoad
            {
                Id = 1,
                LoadId = 1,
                Hours = 5
            };

            Assert.DoesNotThrowAsync(() => service.DeleteAsync(executeLoad), "Delete data aren't valid");
        }

        [Test]
        public void CreateNegative()
        {
            var mockCart = new Mock<IGenericRepository<ExecuteLoadDto, int>>();
            var service = new ExecuteLoadService(mockCart.Object, _autoMapper);
            var executeLoad = new ExecuteLoad
            {
                Id = 2,
                LoadId = 2,
                Hours = -2
            };

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.CreateAsync(executeLoad), "Количество часов не может быть отрицательным");
        }

        [Test]
        public void CreateNegativeAsNull()
        {
            var mockCart = new Mock<IGenericRepository<ExecuteLoadDto, int>>();
            var service = new ExecuteLoadService(mockCart.Object, _autoMapper);
            ExecuteLoad item = null;

            Assert.ThrowsAsync<ArgumentNullException>(() => service.CreateAsync(item), "Value cannot be null. (Parameter 'item')");
        }

        [Test]
        public void UpdateNegative()
        {
            var mockCart = new Mock<IGenericRepository<ExecuteLoadDto, int>>();
            var service = new ExecuteLoadService(mockCart.Object, _autoMapper);
            mockCart.Setup(val => val.GetAllAsync()).ReturnsAsync(new List<ExecuteLoadDto>
            {
                new ExecuteLoadDto { Id = 1,  LoadId = 1, Hours = 5 },
            });

            var executeLoad = new ExecuteLoad
            {
                Id = 1,
                LoadId = 1,
                Hours = -5
            };

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.UpdateAsync(executeLoad), "Количество часов не может быть отрицательным");
        }

        [Test]
        public void UpdateNegativeAsNull()
        {
            var mockCart = new Mock<IGenericRepository<ExecuteLoadDto, int>>();
            var service = new ExecuteLoadService(mockCart.Object, _autoMapper);
            mockCart.Setup(val => val.GetAllAsync()).ReturnsAsync(new List<ExecuteLoadDto>
            {
                new ExecuteLoadDto { Id = 1,  LoadId = 1, Hours = 5 },
            });

            ExecuteLoad item = null;

            Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateAsync(item), "Value cannot be null. (Parameter 'item')");
        }
    }
}
