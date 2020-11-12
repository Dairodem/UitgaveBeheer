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

            return RedirectToAction(nameof(Index));
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
            Contact contact = _contactDatabase.GetContact(id);
            return View(new ContactEditViewModel
            {
                Name = contact.Name,
                Surname = contact.Surname,
                Email = contact.Email,
                BirthDate = contact.BirthDate,
                TelNr = contact.TelNr,
                Address = contact.Address,
                Annotation = contact.Annotation
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
            });

            return RedirectToAction(nameof(Index));
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
    }
}
