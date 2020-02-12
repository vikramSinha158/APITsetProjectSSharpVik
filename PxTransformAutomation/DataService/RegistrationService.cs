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

namespace PxTransformAutomation.DataService
{
    public class RegistrationService: IRegistrationService
    {
         
        public List<EligibleAccounts> GetAuthEligibleAccounts(TranContext tranContext,AccretiveContext accretiveContext)        
        {

            var payorplans=  tranContext.PayorPlans.ToList();

            int? minDaysOut = 0;
            int? maxDaysOut = 5;
            var result33 = (from hipaaConnector in accretiveContext.HIPAAConnectors
                            join hipaaPayorConnector in accretiveContext.HIPAAPayorConnectors on hipaaConnector.ID equals hipaaPayorConnector.ConnectorID
                            join accPayers in accretiveContext.Payors on hipaaPayorConnector.PayorCode.Trim() equals accPayers.PayorCode.Trim()
                            //join payerPlan in tranContext.PayorPlans.ToList() on accPayers.PayorCode == null ? null : accPayers.PayorCode.Trim() equals payerPlan.PayorCode == null ? null : payerPlan.PayorCode.Trim()
                            where hipaaPayorConnector.Status == true && hipaaConnector.Description.Contains("AH Spider") && hipaaConnector.Enabled == true
                            select new
                            {
                                PayorCode = hipaaPayorConnector.PayorCode
                            }).Distinct().ToList();


            var PayorPlanIDs = (from qc1 in payorplans
                                join qc2 in result33 on qc1.PayorCode == null ? null : qc1.PayorCode.Trim() equals qc2.PayorCode == null ? null : qc2.PayorCode.Trim()
                                select new
                                {
                                    Id = qc1.ID
                                }).ToList().Select(x => x.Id).ToList();


            var AuthComplteAccount = (from reg in tranContext.Registrations
                                      join recordStatus in tranContext.RecordTaskStatus.Where(rec => rec.TaskID == 32) on reg.ID equals recordStatus.RecordKey
                                      where reg.AdmitDate >= DateTime.Now.AddDays(minDaysOut.Value) && reg.AdmitDate <= DateTime.Now.AddDays(maxDaysOut.Value) && reg.PatientType != "E" && (recordStatus.Status == 1 || recordStatus.Status == 4)
                                      select new EligibleAccounts
                                      {  
                                          RegistrationID = reg.ID,
                                          EncounterID = reg.EncounterID,
                                          PatientType = reg.PatientType,
                                          AdmitDate = reg.AdmitDate

                                      }).ToList();
          

            var result = (from reg in tranContext.Registrations
                          join cov in tranContext.Coverages on reg.ID equals cov.RegistrationID
                          //join service in tranContext.Services on reg.ID equals service.RegistrationID
                          //join authNeed in tranContext.AuthorizationNeeds on cov.ID equals authNeed.CoverageID
                          where reg.AdmitDate >= DateTime.Now.AddDays(minDaysOut.Value) && reg.AdmitDate <= DateTime.Now.AddDays(maxDaysOut.Value) && reg.PatientType != "E" && PayorPlanIDs.Contains(cov.PayorPlanID.Value) && cov.CoverageStatus == "A" && cov.VerificationStatus == 1 //&& service.IsFinal == true && authNeed.NeedAuth == true
                          select new EligibleAccounts
                          {
                              RegistrationID = reg.ID,
                              EncounterID = reg.EncounterID,
                              PatientType = reg.PatientType,
                              AdmitDate = reg.AdmitDate
                          }).ToList();

            var res = result.Except(AuthComplteAccount).ToList();
            return res;
         
        }
    }
}
