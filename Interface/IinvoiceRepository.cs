using SET_Management.Models.DTO;
using SET_Management.Models.Entity;

namespace SET_Management.Interface
{
    public interface IinvoiceRepository
    {
        ApiResponseDTO checkInvoiceExistById(int invoiceId);
        ApiResponseDTO getInvoiceList();
        ApiResponseDTO addInvoicedetails(mstInvoice invoice);
        ApiResponseDTO editInvoicedetails(mstInvoice invoice);
        ApiResponseDTO deleteInvoicedetails(mstInvoice invoice);
    }
}