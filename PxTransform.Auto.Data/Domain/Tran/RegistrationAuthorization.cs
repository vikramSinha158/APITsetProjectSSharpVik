using System;
using System.Collections.Generic;
using System.Text;

namespace PxTransform.Auto.Data.Domain.Tran
{
    public class RegistrationAuthorization
    {
        public int ID { get; set; }
        public int RegistrationID { get; set; }
        public int AuthorizationID { get; set; }
        public int CoverageID { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int MyProFacilityCodeperty { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
    }
}
