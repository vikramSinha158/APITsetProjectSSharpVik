using System;
using System.Collections.Generic;
using System.Text;

namespace PxTransform.Auto.Data.Domain.Tran
{
    public class Registration
    {

        public int ID { get; set; }
 
        public string EncounterID { get; set; }
        public string PatientType { get; set; }
        public DateTime ? AdmitDate { get; set; }
        public List<Coverage> Coverages { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? IsDischarged { get; set; }
        public string FacilityCode { get; set; }

    }
}
