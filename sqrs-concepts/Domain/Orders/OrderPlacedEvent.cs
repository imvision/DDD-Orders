namespace sqrs_concepts.Domain.Orders
{
    public class OrderPlacedEvent : Event
    {
        public string MarketId { get; }
        public int SelectionId { get; }
        public double Price { get; }
        public double Size { get; }
        public OrderSide Side { get; }
        public DateTime PlaceTime { get; }

        public OrderPlacedEvent(Guid id,
            string marketId,
            int selectionId,
            double price,
            double size,
            OrderSide side,
            DateTime placeTime)
            : base(id, 1)
        {
            MarketId = marketId;
            SelectionId = selectionId;
            Price = price;
            Size = size;
            Side = side;

            PlaceTime = placeTime;
        }
    }
}
