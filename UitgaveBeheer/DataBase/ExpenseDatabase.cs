using UitgaveBeheer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UitgaveBeheer.DataBase;

namespace UitgaveBeheer.Database
{
    public interface IExpenseDatabase
    {
        Expense Insert(Expense expense);
        IEnumerable<Expense> GetExpenses();
        Expense GetExpense(int id);
        void Delete(int id);
        void Update(int id, Expense expense);
    }

    public class ExpenseDatabase : IExpenseDatabase
    {
        private readonly ExpenseDbContext _expenseDbContext;

        public ExpenseDatabase(ExpenseDbContext expenseDbContext)
        {
            _expenseDbContext = expenseDbContext;
        }

        public Expense GetExpense(int id)
        {
            return _expenseDbContext.Expenses.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Expense> GetExpenses()
        {
            return _expenseDbContext.Expenses;
        }

        public Expense Insert(Expense expense)
        {
            _expenseDbContext.Expenses.Add(expense);
            return expense;
        }

        public void Delete(int id)
        {
            var expense = _expenseDbContext.Expenses.SingleOrDefault(x => x.Id == id);
            if (expense != null)
            {
                _expenseDbContext.Expenses.Remove(expense);
            }
        }

        public void Update(int id, Expense updatedExpense)
        {
            var expense = _expenseDbContext.Expenses.SingleOrDefault(x => x.Id == id);
            if (expense != null)
            {
                expense.Omschrijving = updatedExpense.Omschrijving;
                expense.Bedrag = updatedExpense.Bedrag;
                expense.Datum = updatedExpense.Datum;
                expense.Categorie = updatedExpense.Categorie;
                expense.Foto = updatedExpense.Foto;
            }
        }
    }
}
