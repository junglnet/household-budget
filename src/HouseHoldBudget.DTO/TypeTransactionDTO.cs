using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using HouseholdBudget.Common.Entities;

namespace HouseholdBudget.DTO
{
    public class TypeTransactionDTO : DictionaryBase
    {
        public Type Type { get; set; }

    }
}
