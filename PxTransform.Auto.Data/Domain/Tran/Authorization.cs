using System;
using System.Collections.Generic;
using System.Text;

namespace PxTransform.Auto.Data.Domain.Tran
{
    public class Authorization
    {
        public int ID { get; set; }
        public string AuthorizationNumber { get; set; }
        public int? PersonID { get; set; }
        public int? Source { get; set; }
        public string SourceFacilityCode { get; set; }
        public int? SourceAuthorizationID { get; set; }
        public DateTime? DateOfServiceStart { get; set; }
        public DateTime? DateOfServiceEnd { get; set; }
        public string Facility { get; set; }
        public string Procedure { get; set; }
        public string PayorStatus { get; set; }
        public string PayorName { get; set; }
        public string Note { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? UpdatedBy { get; set; }
        public Int16 AuthorizationValidationStatusID { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public bool IsHostUpdated { get; set; }
        public Int16? PlaceOfServiceID { get; set; }
        public string ProcedureUnit { get; set; }
        public string ConnectorPayorName { get; set; }
        public bool IsManualUpdate { get; set; }
    }
}
