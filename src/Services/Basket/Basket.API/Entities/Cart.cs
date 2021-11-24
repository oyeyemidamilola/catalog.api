using System.Collections.Generic;
using System.Linq;

namespace Basket.API.Entities
{
	public class Cart
	{
		public Cart() { }

		public Cart(string userName)
		{
			UserName = userName;
		}
		public string  UserName { get; set; }
		public List<CartItem> Items { get; set; } = new List<CartItem>();

		public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);

	}
}