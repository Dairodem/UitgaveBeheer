using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Schema;
using UitgaveBeheer.Database;
using Microsoft.AspNetCore.Mvc;
using UitgaveBeheer.Domain;
using UitgaveBeheer.Models;
using UitgaveBeheer.Services;
using UitgaveBeheer.Models.DTO;
using AutoMapper;

namespace UitgaveBeheer.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseDatabase _db;
        private readonly IExpenseService _expenseService;
        private readonly IMapper _mapper;
        public ExpenseController(IExpenseDatabase db, IExpenseService expenseService, IMapper mapper)
        {
            _db = db;
            _expenseService = expenseService;
            _mapper = mapper;
        }
        public IActionResult List()
        {
            var expenses = _expenseService.GetAll().Select(x => new ExpenseListViewModel
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

            _expenseService.Create(new ExpenseDto
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
            var expense = _expenseService.GetById(id);
            return View(_mapper.Map<ExpenseDetailsViewModel>(expense));
        }
        public IActionResult Update([FromRoute] int id)
        {
            var expense = _expenseService.GetById(id);
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

            _expenseService.Update(id, new ExpenseDto
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
            var expense = _expenseService.GetById(id);

            return View(new ExpenseDeleteViewModel 
            {
                Id = expense.Id,
                Omschrijving = expense.Omschrijving
            });
        }
        [HttpPost]
        public IActionResult ConfirmDelete([FromRoute] int id)
        {
            _expenseService.Delete(id);
            return RedirectToAction(nameof(List));
        }
        public IActionResult Overview(DateTime? date)
        {
             OverviewDto myView = _expenseService.Overview(date);
            return View(_mapper.Map<OverviewViewModel>(myView));
        }
    }
}
