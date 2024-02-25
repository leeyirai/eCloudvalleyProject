using AutoMapper;
using BusinessLayer;
using BusinessLayer.Model;
using DataLayer;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BusinessLayer
{
    public class BillingLogic : IBillingLogic
    {
        private readonly IBillingManager _billingManager;
        private readonly IMapper _mapper;

        public BillingLogic(IBillingManager billingManager, IMapper mapper)
        {
            _billingManager = billingManager;
            _mapper = mapper;
        }

        public async Task<object> GetDailyLineItemUsageAmountGroupingByProductProductNameAsync(string usageAccountId)
        {
            var data = await _billingManager.QueryAsync(usageAccountId);
            var groupedData = data.GroupBy(item => item.product_productName!).ToDictionary(
                group => group.Key,
                group => group.GroupBy(item => item.lineItem_usageStartDate!.Value.Date).ToDictionary(
                    g => g.Key.ToString("yyyy/MM/dd"),
                    g => g.Sum(item => item.lineItem_usageAmount)));
            return groupedData;
        }

        public async Task<object> GetUnblendedCostGroupByProductNameAsync(string usageAccountId)
        {
            var result = new List<UnblendedCostOutputModel>();
            var data = _mapper.Map<IEnumerable<BillingDto>, IEnumerable<UnblendedCostOutputModel>>(await _billingManager.QueryAsync(usageAccountId));
            var groupedData = data.GroupBy(item => item.product_productName!).ToDictionary(
                group => group.Key,
                group => group.Sum(item => item.lineItem_unblendedCost));
            return groupedData;
        }
    }
}
