using System;
using System.Collections.Generic;
using System.Text;

namespace PxTransform.Auto.Data.Domain.Accretive
{
    public class HIPAAConnector
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string RequesterTypeFullName { get; set; }
        public bool Enabled { get; set; }
        public string Type { get; set; }
        public string Method { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string URL { get; set; }
    }
}
