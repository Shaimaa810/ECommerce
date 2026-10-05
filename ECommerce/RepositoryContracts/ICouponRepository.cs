using ECommerce.DTO;
using ECommerce.Models;

namespace ECommerce.RepositoryContracts
{
	public interface ICouponRepository
	{
		Task Add(Coupon coupon);

		Task Update(Coupon coupon);

		Task Delete(Coupon coupon);

		Task<Coupon?> GetById(int id);

		Task<List<Coupon>?> GetAll();

		Task<Coupon?> GetByCode(string code);

		Task<bool> Exist(int id);
 	}
}
