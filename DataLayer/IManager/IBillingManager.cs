using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IBillingManager
    {
        Task<IEnumerable<BillingDto>> QueryAsync(string usageAccountId);
    }
}
