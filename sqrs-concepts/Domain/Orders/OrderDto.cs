using System;
namespace sqrs_concepts.Domain.Orders
{
	public class OrderDto
	{
		public OrderDto()
		{
		}

		public string MarketId { get; set; }
		public int SelectionId { get; set; }
		public double Price { get; set; }
		public double Size { get; set; }
		public OrderSide Side { get; set; }
	}
}

