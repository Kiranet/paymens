using System.Linq;
using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SparkPayments.Models;

namespace SparkPayments.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly IRepository _repository;

        public PaymentsController(IRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var payments = _repository.List<Payment>().Where(x => x.UserId == User.Identity.Name).ToList();

            if (_repository.List<Category>().Any()) return View(payments);

            _repository.Add(new Category("Comida"));
            _repository.Add(new Category("Carro"));
            _repository.Add(new Category("Salud"));
            _repository.Add(new Category("Otro"));

            return View(payments);
        }

        public IActionResult Create()
        {
            return View("_CreateEdit", new CreateEditPaymentViewModel
            {
                Categories = new SelectList(_repository.List<Category>().Select(x => new {x.Id, x.Name}), "Id", "Name")
            });
        }

        public IActionResult Edit(int id)
        {
            var payment = _repository.GetById<Payment>(id);

            return View("_CreateEdit", new CreateEditPaymentViewModel
            {
                PaymentId = id,
                Amount = payment.Amount,
                Description = payment.Description,
                Categories =
                    new SelectList(_repository.List<Category>().Select(x => new {x.Id, x.Name}),
                        "Id", "Name", id)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CreateEditPaymentViewModel vm)
        {
            _repository.Add(new Payment
            {
                Amount = vm.Amount,
                CategoryId = vm.CategoryId,
                Description = vm.Description,
                UserId = User.Identity.Name,
            });

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(CreateEditPaymentViewModel vm)
        {
            var payment = _repository.GetById<Payment>(vm.PaymentId);

            payment.Amount = vm.Amount;
            payment.Description = vm.Description;
            payment.CategoryId = vm.CategoryId;

            _repository.Update(payment);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var payment = _repository.GetById<Payment>(id);

            _repository.Delete(payment);

            return RedirectToAction("Index");
        }
    }
}