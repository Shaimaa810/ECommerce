using ECommerce.DTO;

namespace ECommerce.ServiceContracts
{
	public interface ICouponService
	{
		Task Add(CouponRequestDTO couponRequestDTO);

		Task Update(CouponUpdateDTO couponUpdateDTO);

		Task Delete(int id);

		Task<List<CouponResponseDTO>?> GetAll();

		Task<CouponResponseDTO?> GetByCode(string code);

		Task<CouponResponseDTO?> GetById(int id);

		Task<bool> Exist(int id);

		Task <dynamic> ApplyCoupon(string userId, string couponCode);  // if coupon is valid return ApplyCouponResponseDTO , if not valid return error message 
	}
}
