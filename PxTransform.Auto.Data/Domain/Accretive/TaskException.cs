using System;
using System.Collections.Generic;
using System.Text;

namespace PxTransform.Auto.Data.Domain.Accretive
{
    public class TaskException
    {
        public int TaskID { get; set; }
        public int ExceptionLevel { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
