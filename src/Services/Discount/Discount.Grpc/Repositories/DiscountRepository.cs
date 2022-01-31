using System.Threading.Tasks;
using Dapper;
using Discount.Grpc.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Grpc.Repositories
{
	public class DiscountRepository : IDiscountRepository
	{
		private readonly IConfiguration _configuration;

		public DiscountRepository(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public async Task<Coupon> GetDiscount(string productName)
		{
			using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
			var sql = "SELECT * FROM Coupon WHERE ProductName = @ProductName";
			var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(sql, new { ProductName = productName });
			return coupon ?? new Coupon { Description = "No discount", Amount = default, ProductName = string.Empty };
		}

		public async Task<bool> CreateDiscount(Coupon coupon)
		{
			using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
			var sql = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES(@ProductName, @Description, @Amount)";
			var executed = await connection.ExecuteAsync(sql, new
			{
				coupon.ProductName,
				coupon.Description,
				coupon.Amount
			});
			return executed > 0;
		}

		public async Task<bool> UpdateDiscount(Coupon coupon)
		{
			using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
			var sql = "UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id";
			var executed = await connection.ExecuteAsync(sql, new
			{
				coupon.Id,
				coupon.ProductName,
				coupon.Description,
				coupon.Amount
			});
			return executed > 0;
		}

		public async Task<bool> DeleteDiscount(string productName)
		{
			using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
			var sql = "DELETE FROM Coupon WHERE ProductName = @ProductName";
			var executed = await connection.ExecuteAsync(sql, new
			{
				ProductName = productName
			});
			return executed > 0;
		}
	}
}