using Discount.Grpc.Protos;

namespace Basket.API.Services
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var couponModel = await _discountProtoService.GetDiscountAsync(new GetDiscountRequest { ProductName = productName });
            return couponModel;

        }
    }
}
