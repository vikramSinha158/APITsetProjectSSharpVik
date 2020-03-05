using PxTransformAutomation.Abstraction;
using PxTransformAutomation.DataService;
using System;
using System.Collections.Generic;
using System.Text;

namespace PxTransformAutomation.Base
{

    class DataCollector
    {
        private Settings _settings;
        public DataCollector(Settings settings)
        {
            _settings = settings;
        }
     
        public ILocationService GetLocationInstance()
        {
            return new LocationService(_settings);
        }
        public IAccountService GetRegisterInstance()
        {
            return new EligibleAccountService();
        }
    }
}
