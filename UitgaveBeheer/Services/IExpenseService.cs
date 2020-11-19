using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UitgaveBeheer.Domain;
using UitgaveBeheer.Models.DTO;

namespace UitgaveBeheer.Services
{
    public interface IExpenseService
    {
        ExpenseDto GetById(int id);
        IEnumerable<ExpenseListDto> GetAll();
        void Create(ExpenseDto expense);
        void Update(int id, ExpenseDto updatedExpense);
        void Delete(int id);
        OverviewDto Overview(DateTime? date);
    }
}
