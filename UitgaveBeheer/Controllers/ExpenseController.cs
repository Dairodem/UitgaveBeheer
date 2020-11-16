using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult Overview()
        {
            var allExpenses = _db.GetExpenses().Where(x => x.Datum.Year == DateTime.Now.Year).Where(x => x.Datum.Month == DateTime.Now.Month);
            if (allExpenses != null)
            {
                Expense highest = allExpenses.OrderByDescending(x => x.Bedrag).First();
                Expense lowest = allExpenses.OrderBy(x => x.Bedrag).First();
                var groupByDay = allExpenses.GroupBy(x => x.Datum);


                return View(new OverviewViewModel
                {
                    Datum = DateTime.Now,
                    OmschrijvingHoogst = highest.Omschrijving,
                    BedragHoogst = highest.Bedrag,
                    OmschrijvingLaagst = lowest.Omschrijving,
                    BedragLaagst = lowest.Bedrag,
                    DatumDag = DateTime.Now,
                    BedragDag = 100.00,
                    CatHoog = "test",
                    BedragCatHoog = 10.00,
                    CatLaag = "Lowtest",
                    BedragCatLaag = 0.00


                });
            }

            return View();
        }
    }
}
