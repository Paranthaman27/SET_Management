
using Microsoft.AspNetCore.Mvc;
using SET_Management.Interface;
using SET_Management.Models.Entity;

namespace SET_Management.Controllers
{
    public class InvoiceController : Controller
    {
        private IApiResponseRepository _apiResponseRepository;
        private IinvoiceRepository _invoiceRepose;
        public InvoiceController(IApiResponseRepository apiResponseRepository, IinvoiceRepository invoiceRepose)
        {
            _apiResponseRepository = apiResponseRepository;
            _invoiceRepose = invoiceRepose;
        }
        public IActionResult Invoice()
        {
            return View();
        }
        public IActionResult GetInvoiceList()
        {
            var Invoices = _invoiceRepose.getInvoiceList();
            return Json(Invoices);
        }

        [HttpGet]
        public IActionResult GetInvoiceById(int id)
        {
            var Invoice = _invoiceRepose.checkInvoiceExistById(id);
            return Json(Invoice);
        }

        [HttpPost]
        public IActionResult SaveInvoice(mstInvoice Invoice)
        {
            if (Invoice.mstInvoiceId == 0)
                return Json(_invoiceRepose.addInvoicedetails(Invoice));
            else
                return Json(_invoiceRepose.editInvoicedetails(Invoice));
        }

        [HttpDelete]
        public IActionResult DeleteInvoice(int id)
        {
            var Invoice = new mstInvoice { mstInvoiceId = id };
            return Json(_invoiceRepose.deleteInvoicedetails(Invoice));
        }
    }
}