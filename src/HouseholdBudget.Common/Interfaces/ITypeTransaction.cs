using HouseholdBudget.Common.Entities;
using System.Threading.Tasks;

namespace HouseholdBudget.Common.Interfaces
{
    public interface ITypeTransaction
    {
        
        decimal GetOperation (Transaction transaction);
        ITypeTransaction GetReverseType();

        string Name { get; set; }
    }

}
