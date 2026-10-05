using ECommerce.DTO;
using ECommerce.Migrations;
using ECommerce.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize (Roles = "Admin")]
	public class CouponController : ControllerBase
	{
		private readonly ICouponService _couponService;
		public CouponController(ICouponService couponService)
		{
			_couponService = couponService;
		}


		[HttpPost]
		public async Task<IActionResult> AddCoupon(CouponRequestDTO couponRequestDTO)
		{
			await _couponService.Add(couponRequestDTO);
			return Ok(new { message = "Coupon created successfully" });
		}


		[HttpPatch("{id}")]
		public async Task<IActionResult> UpdateCoupon(int id, CouponUpdateDTO couponUpdateDto)
		{
			bool exist = await _couponService.Exist(id);
			if (!exist)
			{
				return Problem(detail: "Coupon not found, invalid coupon id", statusCode:404);
			}
			couponUpdateDto.Id = id;
			await _couponService.Update(couponUpdateDto);
			return Ok(new { message = "Coupon updated successfully" });
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCoupon(int id)
		{
			bool exist = await _couponService.Exist(id);
			if (!exist)
			{
				return Problem(detail: "Coupon not found, invalid coupon id", statusCode: 404);
			}
			await _couponService.Delete(id);
			return Ok(new { message = "Coupon deleted successfully" });
		}


		[HttpGet]
		public async Task<ActionResult<List<CouponResponseDTO>?>> GetAll()
		{
			List<CouponResponseDTO>? couponResponseDTOs = await _couponService.GetAll();
			return Ok(couponResponseDTOs);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<CouponResponseDTO?>> GetById(int id)
		{
			CouponResponseDTO? couponResponse = await _couponService.GetById(id);
			if (couponResponse == null)
			{
				return Problem(detail: "Coupon not found, invalid coupon id", statusCode: 404);
			}
			return Ok(couponResponse);
		}

		[HttpGet("get-by-code")]
		public async Task<ActionResult<CouponResponseDTO?>> GetByCode([FromQuery]string code)
		{
			CouponResponseDTO? couponResponse = await _couponService.GetByCode(code);
			if (couponResponse == null)
			{
				return Problem(detail: "Coupon not found, invalid coupon code", statusCode: 404);
			}
			return Ok(couponResponse);
		}


		[HttpGet("apply-coupon")]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> ApplyCoupon([FromQuery]string couponCode) // url => api/coupon/apply-coupon?couponCode=shdjjd
		{
			string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var coupon = await _couponService.GetByCode(couponCode);
			if (coupon == null)
				return NotFound("Coupon not exist, invalid coupon code");
			dynamic result = await _couponService.ApplyCoupon(userId, couponCode);
			if (result is string)
				return BadRequest(result);
			return Ok(result);

		}
	}
}
