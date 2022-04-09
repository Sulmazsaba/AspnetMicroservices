using Basket.API.Entities;
using Basket.API.Repositories;
using Basket.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcService _discountGrpcService;
        public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService)
        {
            _basketRepository = basketRepository;
            _discountGrpcService = discountGrpcService;
        }

        [HttpGet("{username}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCard), StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCard>> GetBasket(string username)
        {
            var basket = await _basketRepository.GetBasket(username);
            return Ok(basket ?? new ShoppingCard(username));

        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCard), StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCard>> UpdateBasket([FromBody] ShoppingCard basket)
        {
            foreach (var cardItem in basket.Items)
            {
                var coupon =await _discountGrpcService.GetDiscount(cardItem.ProductName);
                cardItem.Price -= coupon.Amount;
            }
            return Ok( await _basketRepository.UpdateBasket(basket));
        }

        [HttpDelete("{username}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteBaske(string username)
        {
            await _basketRepository.DeleteBasket(username);
            return Ok();
        }
    }
}
