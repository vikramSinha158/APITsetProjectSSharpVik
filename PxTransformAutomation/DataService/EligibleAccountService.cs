using PxTransform.Auto.Data.Data;
using PxTransformAutomation.Base;
using PxTransformAutomation.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections;
using PxTransform.Auto.Data.Domain.Tran;
using PxTransform.Auto.Data.Domain.Accretive;
using PxTransform.Auto.Data.Domain.Common;

namespace PxTransformAutomation.DataService
{
    public class EligibleAccountService: IAccountService
    {
        #region Methods
        /// <summary>
        /// Methode to get elogible account from database
        /// </summary>
        /// <param name="tranContext"></param>
        /// <param name="accretiveContext"></param>
        /// <param name="minDaysOut"></param>
        /// <param name="maxDaysOut"></param>
        /// <returns></returns>
        public List<EligibleAccounts> GetAuthEligibleAccounts(TranContext tranContext,AccretiveContext accretiveContext, int? minDaysOut, int? maxDaysOut)        
        {

            var payorplans=  tranContext.PayorPlans.ToList();

          
            var PayorPlanID = (from hipaaConnector in accretiveContext.HIPAAConnectors
                            join hipaaPayorConnector in accretiveContext.HIPAAPayorConnectors on hipaaConnector.ID equals hipaaPayorConnector.ConnectorID
                            join accPayers in accretiveContext.Payors on hipaaPayorConnector.PayorCode.Trim() equals accPayers.PayorCode.Trim()
                            where hipaaPayorConnector.Status == true && hipaaConnector.Description.Contains("AH Spider") && hipaaConnector.Enabled == true
                            select new
                            {
                                PayorCode = hipaaPayorConnector.PayorCode
                            }).Distinct().ToList();


            var PayorPlanIDs = (from qc1 in payorplans
                                join qc2 in PayorPlanID on qc1.PayorCode == null ? null : qc1.PayorCode.Trim() equals qc2.PayorCode == null ? null : qc2.PayorCode.Trim()
                                select new
                                {
                                    Id = qc1.ID
                                }).ToList().Select(x => x.Id).ToList();

            var registrations = (from reg in tranContext.Registrations
                                 where reg.IsDischarged == false && (reg.AdmitDate >= DateTime.Now.Date.AddDays(minDaysOut.Value) && reg.AdmitDate <= DateTime.Now.Date.AddDays(maxDaysOut.Value))
                                 && AccountProperty.PatientTypes.Contains(reg.PatientType)
                                 select new
                                 {
                                     RegistrationID = reg.ID,
                                     EncounterID = reg.EncounterID,
                                     PatientType = reg.PatientType,
                                     AdmitDate = reg.AdmitDate,
                                     FacilityCode=reg.FacilityCode

                                 }).Distinct();

            var AuthCompltedAccount = (from reg in registrations
                                       join regAuth in tranContext.RegistrationAuthorizations on reg.RegistrationID equals regAuth.RegistrationID
                                       join auth in tranContext.Authorizations on regAuth.AuthorizationID equals auth.ID
                                       join recordStatus in tranContext.RecordTaskStatus.Where(rec => rec.TaskID == 32) on reg.RegistrationID equals recordStatus.RecordKey
                                       where (recordStatus.Status == 1 || recordStatus.Status == 4)
                                       && auth.AuthorizationValidationStatusID == 1
                                       select new
                                       {
                                           RegistrationID = reg.RegistrationID,
                                           EncounterID = reg.EncounterID,
                                           PatientType = reg.PatientType,
                                           AdmitDate = reg.AdmitDate,
                                           FacilityCode = reg.FacilityCode
                                       }).ToList().Distinct();

            var authRecords = (from reg in registrations
                               join cov in tranContext.Coverages on reg.RegistrationID equals cov.RegistrationID
                               where reg.AdmitDate != null && reg.AdmitDate >= DateTime.Now.Date.AddDays(minDaysOut.Value) && reg.AdmitDate <= DateTime.Now.Date.AddDays(maxDaysOut.Value) && reg.PatientType != "E" && PayorPlanIDs.Contains(cov.PayorPlanID.Value) && cov.CoverageStatus == "A" && cov.VerificationStatus == 1 // && service.IsFinal == true && authNeed.NeedAuth == true
                               select new
                               {
                                   RegistrationID = reg.RegistrationID,
                                   EncounterID = reg.EncounterID,
                                   PatientType = reg.PatientType,
                                   AdmitDate = reg.AdmitDate,
                                   FacilityCode = reg.FacilityCode
                               }).ToList().Except(AuthCompltedAccount);
        


            var EligibleAuthRec = (from em in authRecords
                             select new EligibleAccounts
                             {
                                 RegistrationID = em.RegistrationID,
                                 EncounterID = em.EncounterID,
                                 PatientType = em.PatientType,
                                 AdmitDate = em.AdmitDate,
                                 FacilityCode = em.FacilityCode

                             } ).ToList();

            return EligibleAuthRec;
        }

        /// <summary>
        /// Method to check elgible account ia available in Authschedulerlog
        /// </summary>
        /// <param name="tranContext"></param>
        /// <param name="actualResistrationIDs"></param>
        /// <returns></returns>

        public List<EligibleAccounts> GetEligibleAuthschedulerlog(TranContext tranContext,List<int> actualResistrationIDs)
        {

            var authSchedulerLogResult = (from authScheduler in tranContext.AuthSchedulerLog
                                          where authScheduler.CreatedDateTime > DateTime.Now && actualResistrationIDs.Contains(authScheduler.RegistrationID)
                                          select new EligibleAccounts
                                          {
                                              RegistrationID = authScheduler.RegistrationID

                                          }).Distinct().ToList();
            return authSchedulerLogResult;
        }

        /// <summary>
        /// Method to check elgible account ia available in AuthRequestLog
        /// </summary>
        /// <param name="tranContext"></param>
        /// <param name="actualResistrationIDs"></param>
        /// <returns></returns>

        public List<EligibleAccounts> GetEligibleAuthRequestLog(TranContext tranContext, List<int> actualResistrationIDs)
        {

            var authSchedulerLogResult = (from authScheduler in tranContext.AuthRequestLog
                                          where authScheduler.DateSent > DateTime.Now && actualResistrationIDs.Contains(authScheduler.RegistrationID)
                                          select new EligibleAccounts
                                          {
                                              RegistrationID = authScheduler.RegistrationID

                                          }).Distinct().ToList();
            return authSchedulerLogResult;
        }
        #endregion
    }
}
