using Microsoft.AspNetCore.Mvc;
using OrderService.Exceptions;
using OrderService.Models;
using OrderService.OrderService;

namespace OrderService.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrderServices _orderService;

    public OrderController(ILogger<OrderController> logger, IOrderServices orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }

    /// <summary>
    /// Returns Information On Placed Orders
    /// </summary>
    /// <param name="orderItems"></param>
    /// <returns></returns>
    [HttpPost("availableondate")]
    [ProducesResponseType(typeof(IEnumerable<AvailableDeliveries>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CheckAvailabilityOnDate([FromBody] List<SpecifiedDeliveryOrder> orderItems)
    {        
        var orderId = orderItems.Select(x => x.ProductId).ToList();
        var uniqueOrders = orderItems.Select(x => x.ProductId).Distinct().ToList();

        if (orderId.Count != uniqueOrders.Count)
        {
            return BadRequest("Ordered products must be unique");
        }
        _logger.LogInformation($"Placed order for following product Id's: {string.Join(",", uniqueOrders.ToList())}");
        try
        {
            return Ok(await _orderService.AvalilableDelivery(orderItems));
        }
        catch (IncorrectOrderException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

        /// <summary>
        /// Returns Information On Available Delivery Dates
        /// </summary>
        /// <param name="orderItems"></param>
        /// <returns></returns>
        [HttpPost("availablefordelivery")]
        [ProducesResponseType(typeof(List<AvailableDeliveries>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AvailableDeliveryDates([FromBody] List<OrderRequest> orderItems)
        {
            var orderId = orderItems.Select(x => x.ProductId).ToList();
        
            _logger.LogInformation($"Request information for delivery dates of following products: {string.Join(",", orderId.ToList())}");
            try
            {
                return Ok(await _orderService.AvalilableDeliveryDates(orderItems));
            }
            catch (IncorrectOrderException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
}
