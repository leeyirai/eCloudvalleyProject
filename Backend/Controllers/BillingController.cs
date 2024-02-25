using BusinessLayer;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly IBillingLogic _billingLogic;

        /// <summary>
        /// BillingController_建構子
        /// </summary>
        /// <param name="billingLogic"></param>
        public BillingController(IBillingLogic billingLogic)
        {
            _billingLogic = billingLogic;
        }

        /// <summary>
        /// Get lineItem/UnblendedCost grouping by product/productname
        /// </summary>
        /// <param name="usageAccountId"></param>
        /// <returns>
        /// {
        ///   "{product/productname_A}": "sum(lineitem/unblendedcost)",
        ///   "{product/productname_B}": "sum(lineitem/unblendedcost)",
        ///    ...
        /// }
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetUnblendedCost([FromQuery][Required] string usageAccountId)
            => Ok( await _billingLogic.GetUnblendedCostGroupByProductNameAsync(usageAccountId));

        /// <summary>
        /// Get daily lineItem/UsageAmount grouping by product/productname
        /// </summary>
        /// <param name="usageAccountId"></param>
        /// <returns>
        /// {
        ///   "{product/productname_A}": {
        ///      "YYYY/MM/01": "sum(lineItem/UsageAmount)",
        ///      "YYYY/MM/02": "sum(lineItem/UsageAmount)",
        ///      "YYYY/MM/03": "sum(lineItem/UsageAmount)",
        ///       ...
        ///    },
        ///   "{product/productname_B}": {
        ///      "YYYY/MM/01": "sum(lineItem/UsageAmount)",
        ///      "YYYY/MM/02": "sum(lineItem/UsageAmount)",
        ///      "YYYY/MM/03": "sum(lineItem/UsageAmount)",
        ///       ...
        ///    },
        /// }
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetUsageAmount([FromQuery][Required] string usageAccountId)
            => Ok(await _billingLogic.GetDailyLineItemUsageAmountGroupingByProductProductNameAsync(usageAccountId));
    }
}
