using Dapper;
using Infrastructure;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class SqliteInfra : ISqliteInfra
    {
        private readonly string _connectionString;

        public SqliteInfra()
        {
            _connectionString = GetSqliteConnectString();
        }

        public Task<bool> Delete(string sqlText, IEnumerable<string> id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Insert<T>(string sqlText, T data) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<bool> Insert<T>(string sqlText, IEnumerable<T> dataList) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sqlText, DynamicParameters? parameters) where T : class
        {
            using var cn = new SqliteConnection(_connectionString);
            return await cn.QueryAsync<T>(sql: sqlText, commandType: CommandType.Text, param: parameters);
        }

        public Task<T> QueryFirstOrDefault<T>(string sqlText, DynamicParameters? parameters) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update<T>(string sqlText, T data) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update<T>(string sqlText, IEnumerable<T> dataList) where T : class
        {
            throw new NotImplementedException();
        }

        private string GetSqliteConnectString()
        {
            var projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;
            var dbPath = Path.Combine(projectDirectory, "SqliteBrowser", "eCloudvalley.db");
            return $"Data Source={dbPath}";
        }
    }
}
