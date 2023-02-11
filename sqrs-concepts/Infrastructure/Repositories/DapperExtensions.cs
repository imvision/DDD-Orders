using System;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using System.Data;
using sqrs_concepts.Infrastructure.Streams;
using Dapper;

namespace sqrs_concepts.Infrastructure.Repositories
{
    public static class DapperExtensions
    {
        

        public static async Task<int> ExecuteAsyncWithEvent(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IEventWriter eventWriter = null)
        {
            int result;
            using (var tran = transaction ?? connection.BeginTransaction())
            {
                try
                {
                    result = await connection.ExecuteAsync(sql, param, tran, commandTimeout, commandType);
                    if (eventWriter != null)
                    {
                        // await eventWriter.WriteEvent(new { });
                    }
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }

            return result;
        }
    }
}
