using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface ISqliteInfra
    {
        // 查詢
        Task<IEnumerable<T>> QueryAsync<T>(string sqlText, DynamicParameters? parameters) where T : class;
        Task<T> QueryFirstOrDefault<T>(string sqlText, DynamicParameters? parameters) where T : class;

        // 新增
        Task<bool> Insert<T>(string sqlText, T data) where T : class;
        Task<bool> Insert<T>(string sqlText, IEnumerable<T> dataList) where T : class;

        // 更新
        Task<bool> Update<T>(string sqlText, T data) where T : class;
        Task<bool> Update<T>(string sqlText, IEnumerable<T> dataList) where T : class;
        // 刪除
        Task<bool> Delete(string sqlText, IEnumerable<string> id);
    }
}
