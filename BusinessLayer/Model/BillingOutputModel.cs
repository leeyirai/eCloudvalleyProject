using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model
{
    public class UnblendedCostOutputModel
    {
        public string? product_productName { get; set; }
        public decimal lineItem_unblendedCost { get; set; }
    }
}
