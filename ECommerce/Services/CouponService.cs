using ECommerce.DTO;
using ECommerce.Enums;
using ECommerce.Models;
using ECommerce.RepositoryContracts;
using ECommerce.ServiceContracts;

namespace ECommerce.Services
{
	public class CouponService : ICouponService
	{
		private readonly ICouponRepository _couponRepository;
		private readonly ICartRepository _cartRepository;
		public CouponService(ICouponRepository couponRepository, ICartRepository cartRepository)
		{
			_couponRepository = couponRepository;
			_cartRepository = cartRepository;
		}

		public async Task Add(CouponRequestDTO couponRequestDTO)
		{
			Coupon coupon = new Coupon()
			{
				Code = couponRequestDTO.Code,
				IsActive = couponRequestDTO.IsActive,
				MaxUses = couponRequestDTO.MaxUses,
				UsedCount = couponRequestDTO.UsedCount,
				MinOrderAmount = couponRequestDTO.MinOrderAmount,
				DiscountType = couponRequestDTO.DiscountType,
				Value = couponRequestDTO.Value,
				StartDate = couponRequestDTO.StartDate,
				ExpirationDate = couponRequestDTO.ExpirationDate,
			};
			await _couponRepository.Add(coupon);
		}

		public async Task<dynamic> ApplyCoupon(string userId, string couponCode)
		{
			Cart? cart = await _cartRepository.GetCartWithDetails(userId);
			double totalPrice = cart.CartItems.Sum(ci => ci.ProductVariant.Product.Price);

			Coupon? coupon = await _couponRepository.GetByCode(couponCode);
			string errorMessage = "";
			// check if coupon valid or not
			// check if coupon not expired
			if (!(coupon.StartDate < DateTime.Now && coupon.ExpirationDate > DateTime.Now))
				return "Coupon is not available yet.";
			// check if coupon active or not
			if (!coupon.IsActive)
				return "Coupon is not active.";
			// check if coupon reach max uses or not
			if (coupon.UsedCount == coupon.MaxUses)
				return "Coupon usage limit has been reached.";
			// check if order price > min order amount
			if (totalPrice < coupon.MinOrderAmount)
				return $"Minimum order amount is {coupon.MinOrderAmount}.";

			double discount = 0;
			if (coupon.DiscountType == DiscountType.Fixed)
				discount = coupon.Value;
			else   // discount type -> percentage
				discount = totalPrice * (coupon.Value / 100);

			return new ApplyCouponResponseDTO()
			{
				CouponCode = couponCode,
				Discount = discount,
				TotalBeforeDiscount = totalPrice,
				TotalAfterDiscount = totalPrice - discount,
			};
		
		}

		public async Task Delete(int id)
		{
			Coupon? coupon = await _couponRepository.GetById(id);
			await _couponRepository.Delete(coupon);
		}

		public async Task<bool> Exist(int id)
		{
			return await _couponRepository.Exist(id);
		}

		public async Task<List<CouponResponseDTO>?> GetAll()
		{
			List<Coupon>? coupons = await _couponRepository.GetAll();
			if (coupons == null)
				return new List<CouponResponseDTO>(); // return empty list

			List<CouponResponseDTO> couponsResponseDTO = new List<CouponResponseDTO>();
			foreach (Coupon coupon in coupons)
			{
				couponsResponseDTO.Add(ConvertCouponToCouponResponseDTO(coupon));
			}
			return couponsResponseDTO;
		}

		public async Task<CouponResponseDTO?> GetByCode(string code)
		{
			Coupon? coupon = await _couponRepository.GetByCode(code);
			if (coupon == null) 
				return null;
			return ConvertCouponToCouponResponseDTO(coupon);
			
		}

		public async Task<CouponResponseDTO?> GetById(int id)
		{
			Coupon? coupon = await _couponRepository.GetById(id);
			if (coupon == null)return null;
			return ConvertCouponToCouponResponseDTO(coupon);
		}

		public async Task Update(CouponUpdateDTO couponUpdateDTO)
		{
			Coupon? coupon = await _couponRepository.GetById(couponUpdateDTO.Id);
			coupon.Code = couponUpdateDTO.Code;
			coupon.IsActive = couponUpdateDTO.IsActive;
			coupon.MaxUses = couponUpdateDTO.MaxUses;
			coupon.UsedCount = couponUpdateDTO.UsedCount;
			coupon.MinOrderAmount = couponUpdateDTO.MinOrderAmount;
			coupon.DiscountType = couponUpdateDTO.DiscountType;	
			coupon.Value = couponUpdateDTO.Value;
			coupon.StartDate = couponUpdateDTO.StartDate;
			coupon.ExpirationDate = couponUpdateDTO.ExpirationDate;

			await _couponRepository.Update(coupon);
		}


		private CouponResponseDTO ConvertCouponToCouponResponseDTO(Coupon coupon)
		{
			return new CouponResponseDTO()
			{
				Id = coupon.Id,
				Code = coupon.Code,
				IsActive = coupon.IsActive,
				MaxUses = coupon.MaxUses,
				UsedCount = coupon.UsedCount,
				MinOrderAmount = coupon.MinOrderAmount,
				DiscountType = coupon.DiscountType.ToString(),
				Value = coupon.Value,
				StartDate = coupon.StartDate,
				ExpirationDate = coupon.ExpirationDate,
			};
		}

	}
}
