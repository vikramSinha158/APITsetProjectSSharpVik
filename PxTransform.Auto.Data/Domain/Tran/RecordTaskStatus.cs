using System;
using System.Collections.Generic;
using System.Text;

namespace PxTransform.Auto.Data.Domain.Tran
{
    public class RecordTaskStatus
    {
        public int ID { get; set; }
        public int? TaskID { get; set; }
        public int? Status { get; set; }
        public int RecordKey { get; set; }
    }
}
