using System;
using System.Collections.Generic;
using System.Text;

namespace PxTransform.Auto.Data.Domain.Accretive
{
    public class HIPAAPayorConnector
    {
        public int ID { get; set; }
        public string PayorCode { get; set; }
        public int? ConnectorID { get; set; }
        public int? Order { get; set; }
        public bool? Status { get; set; }
        public DateTime? StatusUpdateDate { get; set; }
        public bool? Enabled { get; set; }
        public string ConnectorPayorId { get; set; }
        public string ConnectorPayorName { get; set; }
        public string TransactionType { get; set; }
        public string URL { get; set; }
        public string Phone { get; set; }
        public Int16? HIPAAVendorID { get; set; }
    }
}
