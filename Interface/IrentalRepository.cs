using SET_Management.Models.DTO;
using SET_Management.Models.Entity;

namespace SET_Management.Interface
{
    public interface IrentalRepository
    {
        ApiResponseDTO checkRentalExistById(int RentalId);
        ApiResponseDTO getRentalList();
        ApiResponseDTO addRentaldetails(mstRentalEntry Rental);
        ApiResponseDTO editRentaldetails(mstRentalEntry Rental);
        ApiResponseDTO deleteRentaldetails(mstRentalEntry Rental);
    }
}