using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using OrderService.Controllers;
using OrderService.Exceptions;
using OrderService.Models;
using OrderService.OrderService;
using Xunit;

namespace OrderService.Tests
{
    public class OrderControllerTest
    {
        private Mock<ILogger<OrderController>> _mocklogger;
        private Mock<IOrderServices> _mockOrderService;        
        private OrderController _sut;

        public OrderControllerTest()
        {
            _mocklogger = new Mock<ILogger<OrderController>>();
            _mockOrderService = new Mock<IOrderServices>();           
            _sut = new OrderController(_mocklogger.Object, _mockOrderService.Object);
        }

        [Theory, AutoData]
        public async Task Should_Return_Available_Delivery_Dates(List<OrderRequest> orders)
        {
            _mockOrderService.Setup(x => x.AvalilableDeliveryDates(orders)).Verifiable();
            var result = await _sut.AvailableDeliveryDates(orders);

            Assert.Equal(StatusCodes.Status200OK, (result as IStatusCodeActionResult)?.StatusCode);
        }

        [Theory, AutoData]
        public async Task Should_Return_Exception_If_Product_Do_Not_Exist(List<OrderRequest> orders)
        {
            _mockOrderService.Setup(x => x.AvalilableDeliveryDates(orders))
                .ThrowsAsync(new IncorrectOrderException("One or More Products Can Not Be Found"))
                                   .Verifiable();

            var result = await _sut.AvailableDeliveryDates(orders);

            Assert.Equal(StatusCodes.Status400BadRequest, (result as IStatusCodeActionResult)?.StatusCode);
        }      
    }
   
}
