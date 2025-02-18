using SET_Management.Models.DTO;
using SET_Management.Models.Entity;

namespace SET_Management.Interface
{
    public interface IvehicleRepository
    {
        ApiResponseDTO addVehicledetails(mstVehicle vehicle);
        ApiResponseDTO editVehicledetails(mstVehicle vehicle);
        ApiResponseDTO deleteVehicledetails(mstVehicle vehicle);
        ApiResponseDTO checkVehicleExistByRegId(int vehicleRegId);
        ApiResponseDTO getVehicleList();
    }
}