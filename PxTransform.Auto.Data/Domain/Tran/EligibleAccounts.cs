using System;
using System.Collections.Generic;
using System.Text;

namespace PxTransform.Auto.Data.Domain.Tran
{
    public class EligibleAccounts
    {
       // public int ID { get; set; }
        public int RegistrationID { get; set; }
        public string EncounterID { get; set; }
        public string PatientType { get; set; }
        public DateTime? AdmitDate { get; set; }
        public string FacilityCode { get; set; }
    }
}
