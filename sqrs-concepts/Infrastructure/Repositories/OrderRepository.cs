using System;
using sqrs_concepts.Infrastructure.Streams;

namespace sqrs_concepts.Infrastructure.Repositories
{
	public class OrderRepository : BaseRepository
	{
		public OrderRepository(IEventWriter eventWriter)
			: base(eventWriter)
		{
		}
	}
}
