using PxTransform.Auto.Data.Data;
using PxTransform.Auto.Data.Domain.Tran;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PxTransformAutomation.Abstraction
{
    public interface IAccountService
    {
        /// <summary>
        /// Define the method to get eligible Account
        /// </summary>
        /// <param name="tranContext"></param>
        /// <param name="accretiveContext"></param>
        /// <param name="minDaysOut"></param>
        /// <param name="maxDaysOut"></param>
        /// <returns></returns>
        List<EligibleAccounts> GetAuthEligibleAccounts(TranContext tranContext, AccretiveContext accretiveContext, int? minDaysOut, int? maxDaysOut);

        /// <summary>
        /// Define the method elgible account ia available in Authschedulerlog
        /// </summary>
        /// <param name="tranContext"></param>
        /// <param name="actualResistrationIDs"></param>
        /// <returns></returns>
        List<EligibleAccounts> GetEligibleAuthschedulerlog(TranContext tranContext, List<int> actualResistrationIDs);


        /// <summary>
        /// Define the methodelgible account ia available in AuthRequestLog
        /// </summary>
        /// <param name="tranContext"></param>
        /// <param name="actualResistrationIDs"></param>
        /// <returns></returns>
        List<EligibleAccounts> GetEligibleAuthRequestLog(TranContext tranContext, List<int> actualResistrationIDs);
    }
}
