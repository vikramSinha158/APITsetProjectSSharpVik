using System;
using System.Collections.Generic;
using System.Text;

namespace PxTransform.Auto.Data.Domain.Tran
{
    public class PayorPlan
    {
        public int ID { get; set; }
        public string ProductLineCode { get; set; }
        public string FacilityPlanCode { get; set; }
        public string PlanType { get; set; }
        public string PayorCode { get; set; }
        public string PayorPlanCode { get; set; }
        public string PayorPlanName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
    }
}
