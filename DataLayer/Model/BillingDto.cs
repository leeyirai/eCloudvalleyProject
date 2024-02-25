using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class BillingDto
    {
        public int pkno { get; set; }
        public long? bill_payerAccountId { get; set; }
        public decimal lineItem_unblendedCost { get; set; }
        public string? lineItem_unblendedRate { get; set; }
        public long lineItem_usageAccountId { get; set; }
        public decimal lineItem_usageAmount { get; set; }
        public DateTime? lineItem_usageStartDate { get; set; }
        public DateTime? lineItem_usageEndDate { get; set; }
        public string? product_productName{ get; set; }
    }
}
