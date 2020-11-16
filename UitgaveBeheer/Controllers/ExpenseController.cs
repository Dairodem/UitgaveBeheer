using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Schema;
using ASPCore02.Database;
using Microsoft.AspNetCore.Mvc;
using UitgaveBeheer.Domain;
using UitgaveBeheer.Models;

namespace UitgaveBeheer.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseDatabase _db;
        public ExpenseController(IExpenseDatabase db)
        {
            _db = db;
        }
        public IActionResult List()
        {
            var expenses = _db.GetExpenses().Select(x => new ExpenseListViewModel
            { Id = x.Id, Omschrijving = x.Omschrijving, Bedrag = x.Bedrag, Datum = x.Datum}).OrderByDescending(x => x.Datum);
            
            return View(expenses);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create([FromForm] ExpenseCreateViewModel model)
        {
            if (!TryValidateModel(model))
            {
            return View();
            }

            _db.Insert(new Expense
            {
                Omschrijving = model.Omschrijving,
                Bedrag = model.Bedrag,
                Datum = model.Datum,
                Categorie = model.Categorie
            });

            return RedirectToAction(nameof(List));
        }
        public IActionResult Details([FromRoute] int id)
        {
            Expense expense = _db.GetExpense(id);
            return View(new ExpenseDetailsViewModel 
            {
                Id = expense.Id,
                Omschrijving = expense.Omschrijving,
                Bedrag = expense.Bedrag,
                Datum = expense.Datum,
                Categorie = expense.Categorie

            });
        }
        public IActionResult Update([FromRoute] int id)
        {
            Expense expense = _db.GetExpense(id);
            return View(new ExpenseUpdateViewModel
            {
                Omschrijving = expense.Omschrijving,
                Bedrag = expense.Bedrag,
                Datum = expense.Datum,
                Categorie = expense.Categorie
            });
        }
        [HttpPost]
        public IActionResult Update([FromForm] ExpenseUpdateViewModel vm, [FromRoute] int id)
        {
            if (!TryValidateModel(vm))
            {
                return View(vm);
            }

            _db.Update(id, new Expense
            {
                Omschrijving = vm.Omschrijving,
                Bedrag = vm.Bedrag,
                Categorie = vm.Categorie,
                Datum = vm.Datum
            });

            return RedirectToAction(nameof(List));
        }
        public IActionResult Delete([FromRoute] int id)
        {
            Expense expense = _db.GetExpense(id);

            return View(new ExpenseDeleteViewModel 
            {
                Id = expense.Id,
                Omschrijving = expense.Omschrijving
            });
        }
        [HttpPost]
        public IActionResult ConfirmDelete([FromRoute] int id)
        {
            _db.Delete(id);
            return RedirectToAction(nameof(List));
        }
        public IActionResult Overview(DateTime? date)
        {
            DateTime myDate = (DateTime)date;
            
            var allExpenses = _db.GetExpenses().Where(x => x.Datum.Year == myDate.Year).Where(x => x.Datum.Month == myDate.Month);
            if (allExpenses.Count() != 0)
            {
                Expense highest = allExpenses.OrderByDescending(x => x.Bedrag).FirstOrDefault();
                Expense lowest = allExpenses.OrderBy(x => x.Bedrag).FirstOrDefault();
                var groupByDay = allExpenses.GroupBy(x => x.Datum);

                double max = 0;
                DateTime day = new DateTime();
                foreach (var item in groupByDay)
                {
                    double total = 0;

                    foreach (Expense expense in item)
                    {
                        total += expense.Bedrag; 
                    }
                    if (total > max)
                    {
                        max = total;
                        day = item.Key;
                    }
                }

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

                return View(new OverviewViewModel
                {
                    Datum = myDate,
                    IdHoogst = highest.Id,
                    OmschrijvingHoogst = highest.Omschrijving,
                    BedragHoogst = highest.Bedrag,
                    IdLaagst = lowest.Id,
                    OmschrijvingLaagst = lowest.Omschrijving,
                    BedragLaagst = lowest.Bedrag,
                    DatumDag = day,
                    BedragDag = mostExpensiveday.Value,
                    CatHoog = highCatN,
                    BedragCatHoog = highCatV,
                    CatLaag = lowCatN,
                    BedragCatLaag = lowCatV


                });
            }

            return View(new OverviewViewModel { Datum = myDate});
        }
    }
}
