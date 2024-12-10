using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Xunit;
using Repositories.InterfaceRepositories;
using Repositories.Repositories;
using Models;
using System.Xml.Linq;
using DtoModelsProj;

namespace Tests
{
    public class OrderRepositoryTests : IDisposable
    {
        private static int _databaseCounter = 0;
        private readonly AppDbContext context;
        private readonly IOrderRepository orderRepository;

        public OrderRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

            context = new AppDbContext(options);
            orderRepository = new OrderRepository(context);
        }

        [Fact]
        public async Task CreateNewOrder_ValidOrder_ReturnsCreatedOrderId()
        {
            // Arrange
            var user = new User { Id = 1, Name = "User1", Email = "User1@example.com", Password = "Password", PhoneNumber = "1234567890" }; //Создал пользователя, чтобы иметь ввиду UserId
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var newOrderDto = new CreateOrderDto
            {
                Dto_UserIdCreated = user.Id,
                Dto_TitleName = "Test Order",
                Dto_Adress = "Test Address",
                Dto_DayToDelay = 3,
                Dto_ContactInformation = "Test Contact",
                Dto_OrderPriorityId = "M"
            };

            // Act
            var result = await orderRepository.CreateNewOrder(newOrderDto);

            // Assert
            // Проверяю создался ли заказ, создался и передал ли он id заказа
            Assert.True(result.IsCreated);
            Assert.NotEqual(0, result.OrderId);
        }

        [Fact]
        public async Task CreateNewOrder_InvalidUser_ReturnsNotCreated()
        {
            // Arrange
            var newOrderDto = new CreateOrderDto
            {
                Dto_UserIdCreated = 999, // Некорректный UserId
                Dto_TitleName = "Test Order",
                Dto_Adress = "Test Address",
                Dto_DayToDelay = 3,
                Dto_ContactInformation = "Test Contact",
                Dto_OrderPriorityId = "M"
            };

            // Act
            var result = await orderRepository.CreateNewOrder(newOrderDto);

            // Assert
            Assert.False(result.IsCreated);
            Assert.Equal(0, result.OrderId); // OrderId остается равным 0
        }

        public void Dispose()
        {
            // Очищаем базу данных перед каждым тестом
            context.Database.EnsureDeleted();
        }
    }
}
