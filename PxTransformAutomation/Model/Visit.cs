using System;
using System.Collections.Generic;
using System.Text;

namespace PxTransformAutomation.Model
{
    public class Visit
    {

        public string FacilityCode { get; set; }
        public string VisitNumber { get; set; }
        public string VisitType { get; set; }
        public DateTime StartDateTime { get; set; }
        public string PatientMRN { get; set; }
        public string EnterpriseID { get; set; }
        public bool InScope { get; set; }
        public object InsuranceCoverage { get; set; }
        public string HospitalServiceLocation { get; set; }
        public object EstimatedServices { get; set; }
    }
}
