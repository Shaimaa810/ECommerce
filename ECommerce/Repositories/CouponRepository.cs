using ECommerce.Models;
using ECommerce.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories
{
	public class CouponRepository : ICouponRepository
	{
		private readonly ApplicationDbContext _dbcontext;
		public CouponRepository(ApplicationDbContext dbcontext)
		{
			_dbcontext = dbcontext;
		}


		public async Task Add(Coupon coupon)
		{
			await _dbcontext.AddAsync(coupon);
			await _dbcontext.SaveChangesAsync();
		}

		public async Task Delete(Coupon coupon)
		{
			_dbcontext.Remove(coupon);
			await _dbcontext.SaveChangesAsync();
		}

		public async Task<bool> Exist(int id)
		{
			return await _dbcontext.Coupons.AnyAsync(c => c.Id == id);
		}

		public async Task<List<Coupon>?> GetAll()
		{
			return await _dbcontext.Coupons.ToListAsync();
		}

		public async Task<Coupon?> GetByCode(string code)
		{
			return await _dbcontext.Coupons.FirstOrDefaultAsync(c => c.Code == code);
		}

		public async Task Update(Coupon coupon)
		{
			_dbcontext.Update(coupon);
			await _dbcontext.SaveChangesAsync();
		}

		public async Task<Coupon?> GetById(int id)
		{
			return await _dbcontext.Coupons.FirstOrDefaultAsync(c => c.Id == id);
		}
	}
}
