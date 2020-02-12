using System;
using System.Collections.Generic;
using System.Text;

namespace PxTransform.Auto.Data.Domain.Tran
{
   public  class Coverage
    {
        public int ID { get; set; }
        public int RegistrationID { get; set; }
        public string CoverageStatus { get; set; }
        public int VerificationStatus { get; set; }
        public int? PayorPlanID { get; set; }
        public Int16? COBOrder { get; set; }
    }
}
