using System;
using System.Collections.Generic;
using System.Text;

namespace PxTransform.Auto.Data.Domain.Accretive
{
    public class Location
    {
        public int ID { get; set; }
        public string Code { get; set; }

        public string ConnectionString { get; set; }
        public string Type { get; set; }
        public string ServerPath { get; set; }
    }
}
