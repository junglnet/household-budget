using System;

namespace HouseholdBudget.Common.Interfaces
{
    public interface ITransaction 
    {
        decimal GetOperation();

        /// <summary>
        /// Название транзакции
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Запланированная сумма
        /// </summary>
        decimal PlannedSum { get; set; }

        /// <summary>
        /// Фактическая сумма
        /// </summary>
        decimal FactSum { get; set; }

        /// <summary>
        /// Дата и время операции
        /// </summary>
        DateTime DateTime { get; set; }

        /// <summary>
        /// Источник транзации
        /// </summary>
        IBudgetaryFund SourceBudgetaryFund { get; set; }

        /// <summary>
        /// Получатель транзакции
        /// </summary>
        IBudgetaryFund EndPointBudgetaryFund { get; set; }
    }
}
