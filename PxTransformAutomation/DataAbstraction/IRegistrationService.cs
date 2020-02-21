using PxTransform.Auto.Data.Data;
using PxTransform.Auto.Data.Domain.Tran;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PxTransformAutomation.Abstraction
{
    public interface IRegistrationService
    {
        List<EligibleAccounts> GetAuthEligibleAccounts(TranContext tranContext, AccretiveContext accretiveContext);

        List<EligibleAccounts> GetEligibleAuthschedulerlog(TranContext tranContext, List<int> actualResistrationIDs);

        List<EligibleAccounts> GetEligibleAuthRequestLog(TranContext tranContext, List<int> actualResistrationIDs);
    }
}
