using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PxTransform.Auto.Data.Domain.Tran
{
    public class AuthSchedulerLog
    {
        [Key]
        public int AuthSchedulerLogID { get; set; }
        public int RegistrationID { get; set; }
        public Int16 SchedulerDay { get; set; }
        public DateTime? StartDateTime { get; set; }

        public DateTime? CompleteDateTime { get; set; }

        public DateTime? CreatedDateTime { get; set; }
    }
}
