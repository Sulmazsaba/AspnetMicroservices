using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _discountRepository.GetDiscount(request.ProductName);

            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"product with product name : {request.ProductName} not found"));
            }

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public async override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {

            var coupon = _mapper.Map<Coupon>(request.Coupon);

            await _discountRepository.CreateDiscount(coupon);
            _logger.LogInformation("Coupon Created for productName ={productName} ", coupon.ProductName);

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public async override Task<CouponModel> UpdateDiscount(UpdateDisountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            await _discountRepository.UpdateDiscount(coupon);
            _logger.LogInformation("Coupon Updated for productName ={productName} ", coupon.ProductName);

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDisountRequest request, ServerCallContext context)
        {
            var succeeded = await _discountRepository.DeleteDiscount(request.ProductName);

            if(succeeded)
            _logger.LogInformation("Coupon Deleted for productName ={productName} ", request.ProductName);


            return new DeleteDiscountResponse { Success = succeeded };

        }
    }
}
