using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using sqrs_concepts.Domain;
using sqrs_concepts.Infrastructure.Streams;

namespace sqrs_concepts.Infrastructure.Repositories
{
    public abstract class BaseRepository
	{
		private readonly IEventWriter eventWriter;

		public BaseRepository(IEventWriter eventWriter)
		{
			this.eventWriter = eventWriter;
		}

        public async Task<int> SaveAggregateAsync(string sql,
            AggregateBase aggregate,
            IDbTransaction? transaction = null)
        {
            using var connection = transaction?.Connection as SqlConnection ?? new SqlConnection();
            await connection.OpenAsync();

            using var currentTransaction = transaction ?? await connection.BeginTransactionAsync();
            try
            {
                var result = await connection.ExecuteAsync(sql, aggregate);

                var events = aggregate.GetEvents();
                foreach (var @event in events)
                {
                    await eventWriter.WriteEvent(@event);
                }

                currentTransaction.Commit();

                return result;
            }
            catch
            {
                currentTransaction.Rollback();
                throw;
            }
        }
    }
}
