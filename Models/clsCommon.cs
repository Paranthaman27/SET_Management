using System;
namespace SET_Management.Models
{
    public class clsCommon
    {
        public static int iSuperAdminId { get; set; } = 1;
        public static int iAdminUserId { get; set; } = 2;
        public static int iDrieverUserId { get; set; } = 3;
        public static int iGuestUserId { get; set; } = 4;

        public const string sSuperAdmin = "SuperAdmin";
        public const string sAdmin = "Admin";
        public const string sDriever = "Driver";
        public const string sGuest = "Guest";

        public static string getUserType(int? userTypeId)
        {
            if (userTypeId == iSuperAdminId)
            {
                return sSuperAdmin;
            }
            else if (userTypeId == iAdminUserId)
            {
                return sAdmin;
            }
            else if (userTypeId == iDrieverUserId)
            {
                return sDriever;
            }
            else
            {
                return sGuest;
            }
        }
        public class clsResponse
        {
            public static string sDatafetchedSuccess { get; set; } = "Data Fetched Successfully";
            public static string sDataSaveResult { get; set; } = "Data Saved Successfully";
            public static string sDataSavedFailure { get; set; } = "Data Saveing process  Failed";

        }
    }
}