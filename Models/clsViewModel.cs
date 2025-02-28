using SET_Management.Models.Entity;

namespace SET_Management.Models
{
    public class clsViewModel
    {

    }
    public class clsRentalViewModel
    {
        public List<mstVehicle> lstDrpVehicle;
        public List<mstCompany> lstDrpCompany;
    }
    public class clsInvoiceViewModel
    {
        public List<mstVehicle> lstDrpVehicle;
        public List<mstCompany> lstDrpCompany;
        public List<mstRentalEntry> rentalList;
    }
}