namespace sqrs_concepts.Domain.Orders
{
	public class Order : AggregateBase
	{
		public string MarketId { get; private set; }
		public int SelectionId { get; private set; }
		public double Price { get; private set; }
		public double Size { get; private set; }
		public OrderSide Side { get; private set; }
		public DateTime PlaceTime { get; private set; }

		public Order(Guid id,
			string marketId,
			int selectionId,
			double price,
			double size,
			OrderSide side)
		{
			Id = id;
			Version = 1;

			MarketId = marketId;
			SelectionId = selectionId;
			Price = price;
			Size = size;
			Side = side;

			PlaceTime = DateTime.UtcNow;

			ApplyEvent(new OrderPlacedEvent(
				Id,
				MarketId,
				SelectionId,
				Price,
				Size,
				Side,
				PlaceTime));
		}
	}
}

