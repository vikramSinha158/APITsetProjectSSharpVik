using PxTransformAutomation.Utilities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using Microsoft.Extensions.Configuration;
using PxTransform.Auto.Data.Data;
using PxTransform.Auto.Data.Domain.Accretive;
using PxTransformAutomation.Reports;

namespace PxTransformAutomation.Base
{
    public class Settings
    {
        public Uri BaseUrl { get; set; }
        public IRestResponse Response { get; set; }
        public IRestRequest Request { get; set; }
        public RestClient RestClient { get; set; } = new RestClient();
        public Libraries lib { get; set; } = new Libraries();
        public Utility Util { get; set; } = new Utility();
        public Dictionary<string, string> ParameterList { get; set; } = new Dictionary<string, string>();
        public IConfigurationRoot config { get; set; }

        public ReportConfiguration Report = new ReportConfiguration();






    }
}
