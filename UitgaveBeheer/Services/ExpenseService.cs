
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UitgaveBeheer.Database;
using UitgaveBeheer.Domain;
using UitgaveBeheer.Models.DTO;

namespace UitgaveBeheer.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseDatabase _db;
        public ExpenseService(IExpenseDatabase db)
        {
            _db = db;
        }
        public ExpenseDto GetById(int id)
        {
            var expense = _db.GetExpense(id);
            return new ExpenseDto
            {
                Id = expense.Id,
                Bedrag = expense.Bedrag,
                Omschrijving = expense.Omschrijving,
                Datum = expense.Datum,
                Categorie = expense.Categorie
            };
        }

        public IEnumerable<ExpenseListDto> GetAll()
        {
            return  _db.GetExpenses().Select(x => new ExpenseListDto
            { Id = x.Id, Omschrijving = x.Omschrijving, Bedrag = x.Bedrag, Datum = x.Datum });

        }
        public void Create(ExpenseDto expense)
        {
            _db.Insert(new Expense
            {
                Omschrijving = expense.Omschrijving,
                Bedrag = expense.Bedrag,
                Datum = expense.Datum,
                Categorie = expense.Categorie
            });
        }
        public void Update(int id, ExpenseDto updatedExpense)
        {
            _db.Update(id, new Expense
            {
                Omschrijving = updatedExpense.Omschrijving,
                Bedrag = updatedExpense.Bedrag,
                Categorie = updatedExpense.Categorie,
                Datum = updatedExpense.Datum
            });
        }

        public void Delete(int id)
        {
            _db.Delete(id);
        }

        public OverviewDto Overview(DateTime? date)
        {
            DateTime myDate = (DateTime)date;

            var allExpenses = _db.GetExpenses().Where(x => x.Datum.Year == myDate.Year).Where(x => x.Datum.Month == myDate.Month);
            if (allExpenses.Count() != 0)
            {
                Expense highest = allExpenses.OrderByDescending(x => x.Bedrag).FirstOrDefault();
                Expense lowest = allExpenses.OrderBy(x => x.Bedrag).FirstOrDefault();

                //andere manier om duurste dag van maand te verkrijgen
                var mostExpensiveday = allExpenses.GroupBy(x => x.Datum).Select(x => new { Date = x.Key, Value = x.ToList().Sum(x => x.Bedrag) })
                    .OrderByDescending(x => x.Value).FirstOrDefault();


                var cat = allExpenses.GroupBy(x => x.Categorie);
                double highCatV = 0;
                double lowCatV = double.MaxValue;
                string highCatN = "";
                string lowCatN = "";
                foreach (var item in cat)
                {
                    double total = 0;
                    foreach (Expense expense in item)
                    {
                        total += expense.Bedrag;
                    }
                    if (total > highCatV)
                    {
                        highCatV = total;
                        highCatN = item.Key;
                    }
                    if (total < lowCatV)
                    {
                        lowCatV = total;
                        lowCatN = item.Key;
                    }
                }

                return new OverviewDto
                {
                    Datum = myDate,
                    IdHoogst = highest.Id,
                    OmschrijvingHoogst = highest.Omschrijving,
                    BedragHoogst = highest.Bedrag,
                    IdLaagst = lowest.Id,
                    OmschrijvingLaagst = lowest.Omschrijving,
                    BedragLaagst = lowest.Bedrag,
                    DatumDag = mostExpensiveday.Date,
                    BedragDag = mostExpensiveday.Value,
                    CatHoog = highCatN,
                    BedragCatHoog = highCatV,
                    CatLaag = lowCatN,
                    BedragCatLaag = lowCatV


                };
            }

            return new OverviewDto();
        }
    }
    
}
