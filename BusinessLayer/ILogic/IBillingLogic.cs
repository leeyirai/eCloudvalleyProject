using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IBillingLogic
    {
        Task<object> GetUnblendedCostGroupByProductNameAsync(string usageAccountId);
        Task<object> GetDailyLineItemUsageAmountGroupingByProductProductNameAsync(string usageAccountId);
    }
}
