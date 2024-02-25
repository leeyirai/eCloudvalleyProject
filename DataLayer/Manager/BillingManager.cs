using DataLayer.Model;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class BillingManager : IBillingManager
    {
        private readonly ISqliteInfra _sqliteInfra;

        public BillingManager(ISqliteInfra sqliteInfra)
        {
            _sqliteInfra = sqliteInfra;
        }

        public async Task<IEnumerable<BillingDto>> QueryAsync(string usageAccountId)
        {
            var parameters = new Dapper.DynamicParameters();
            var sqlWhere = new StringBuilder();
            var sqlOrderBy = new StringBuilder();

            if (!string.IsNullOrEmpty(usageAccountId))
            {
                sqlWhere.Append(" WHERE lineItem_usageAccountId = @lineItem_usageAccountId ");
                parameters.Add("lineItem_usageAccountId", usageAccountId);
            }

            var sqlText = @"SELECT pkno
                ,bill_payerAccountId 
                ,lineItem_unblendedCost 
                ,lineItem_unblendedRate 
                ,lineItem_usageAccountId 
                ,lineItem_usageAmount 
                ,lineItem_usageStartDate 
                ,lineItem_usageEndDate 
                ,product_productName 
                FROM aws_billing
            ";

            sqlOrderBy.Append(" ORDER BY lineItem_usageAccountId, product_productName, lineItem_usageStartDate ");

            return await _sqliteInfra.QueryAsync<BillingDto>(sqlText.ToString() + sqlWhere.ToString() + sqlOrderBy.ToString(), parameters);
        }
    }
}
