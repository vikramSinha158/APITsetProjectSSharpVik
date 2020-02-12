using System;
using System.Collections.Generic;
using System.Text;

namespace PxTransform.Auto.Data.Domain.Tran
{
    public class AuthorizationNeed
    {
        public int ID { get; set; }
        public bool? NeedAuth { get; set; }
        public int? CoverageID { get; set; }
        public int? TaskID { get; set; }
        public int? ProcessID { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
