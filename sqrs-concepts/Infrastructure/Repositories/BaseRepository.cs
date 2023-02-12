using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using sqrs_concepts.Domain;
using sqrs_concepts.Infrastructure.Streams;

namespace sqrs_concepts.Infrastructure.Repositories
{
    public abstract class BaseRepository
	{
        private const string _connectionString = "Server=tcp:sqlserver,1433;Database=Orders;User ID=sa;Password=Password1234!;Trusted_Connection=False;Encrypt=False;";


        private readonly IEventWriter eventWriter;

		public BaseRepository(IEventWriter eventWriter)
		{
			this.eventWriter = eventWriter;
		}

        public async ValueTask<int> SaveAggregateAsync(string sql,
            AggregateBase aggregate,
            IDbTransaction? transaction = null)
        {
            using var connection = transaction?.Connection as SqlConnection ?? new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var currentTransaction = transaction ?? await connection.BeginTransactionAsync();
            try
            {
                var result = await connection.ExecuteAsync(sql, aggregate, transaction: currentTransaction);

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
