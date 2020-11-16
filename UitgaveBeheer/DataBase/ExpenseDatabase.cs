using UitgaveBeheer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCore02.Database
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
        private int _counter;
        private readonly List<Expense> _expenses;

        public ExpenseDatabase()
        {
            if (_expenses == null)
            {
                _expenses = new List<Expense>();
                Insert(new Expense { Omschrijving = "Tankbeurt", Bedrag = 78.45, Datum = new DateTime(2020, 10, 20), Categorie = "Wagen" });
                Insert(new Expense { Omschrijving = "Tankbeurt", Bedrag = 82.12, Datum = new DateTime(2020, 10, 10), Categorie = "Wagen" });
                Insert(new Expense { Omschrijving = "Tankbeurt", Bedrag = 100.36, Datum = new DateTime(2020, 9, 15), Categorie = "Wagen" });
                Insert(new Expense { Omschrijving = "Colruyt", Bedrag = 87.33, Datum = new DateTime(2020, 10, 11), Categorie = "Boodschappen" });
                Insert(new Expense { Omschrijving = "Colruyt", Bedrag = 94.04, Datum = new DateTime(2020, 9, 15), Categorie = "Boodschappen" });
                Insert(new Expense { Omschrijving = "Delhaize", Bedrag = 214.97, Datum = new DateTime(2020, 9, 23), Categorie = "Boodschappen" });
                Insert(new Expense { Omschrijving = "Elektriciteit", Bedrag = 125.95, Datum = new DateTime(2020,10,20), Categorie = "Energie" });
                Insert(new Expense { Omschrijving = "Elektriciteit", Bedrag = 125.95, Datum = new DateTime(2020, 9, 20), Categorie = "Energie" });
                Insert(new Expense { Omschrijving = "Tankbeurt", Bedrag = 98.45, Datum = new DateTime(2020, 11, 19), Categorie = "Wagen" });
                Insert(new Expense { Omschrijving = "Delhaize", Bedrag = 214.97, Datum = new DateTime(2020, 11, 19), Categorie = "Boodschappen" });
                Insert(new Expense { Omschrijving = "Elektriciteit", Bedrag = 125.95, Datum = new DateTime(2020, 11, 20), Categorie = "Energie" });
            }
        }

        public Expense GetExpense(int id)
        {
            return _expenses.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Expense> GetExpenses()
        {
            return _expenses;
        }

        public Expense Insert(Expense expense)
        {
            _counter++;
            expense.Id = _counter;
            _expenses.Add(expense);
            return expense;
        }

        public void Delete(int id)
        {
            var expense = _expenses.SingleOrDefault(x => x.Id == id);
            if (expense != null)
            {
                _expenses.Remove(expense);
            }
        }

        public void Update(int id, Expense updatedExpense)
        {
            var expense = _expenses.SingleOrDefault(x => x.Id == id);
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
