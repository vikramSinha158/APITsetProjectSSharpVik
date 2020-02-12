using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PxTransform.Auto.Data.Domain.Accretive
{
    public class Payor
    {
        [Key]
        public string PayorCode { get; set; }
        public string PayorName { get; set; }
        public bool? IsAutomated { get; set; }
        public string EmdeonPayorId { get; set; }
        public string EmdeonPayorName { get; set; }
        public string EmdeonDisclaimer { get; set; }
        public int? PayorStatus { get; set; }
        public string RequesterTypeFullName { get; set; }
    }
}
