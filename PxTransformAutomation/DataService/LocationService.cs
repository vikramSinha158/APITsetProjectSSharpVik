using Microsoft.EntityFrameworkCore;
using PxTransform.Auto.Data.Data;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using PxTransformAutomation.Base;

namespace PxTransformAutomation.DataService
{
    class LocationService : ILocationService
    {
        private Settings _settings;

        #region Ctor
        public LocationService(Settings settings)
        {
            _settings = settings;
        }
        #endregion

        #region Methods
        public AccretiveContext GetAccretiveContext()
        {
            var optionsBuildertran = new DbContextOptionsBuilder<AccretiveContext>();
            string dddd = _settings.Util.TranViewDataServiceUrl["ConnectionStrings:AccretiveConnection"];
            optionsBuildertran.UseSqlServer(_settings.Util.TranViewDataServiceUrl["ConnectionStrings:AccretiveConnection"]);
            return new AccretiveContext(optionsBuildertran.Options);
        }

        private string GetTranConnection(string facilityCode)
        {
            var location = GetAccretiveContext().Locations.Where(x => x.Code == facilityCode).FirstOrDefault();
            string TranConnectionFormat = _settings.Util.TranViewDataServiceUrl["ConnectionStrings:TranConnectionformat"];
            return string.Format(TranConnectionFormat, location.ConnectionString.ToString().Replace("[", "").Replace("]", ""), location.Code);
        }

        public TranContext GetTranContext(string facilityCode)
        {
            var optionsBuildertran = new DbContextOptionsBuilder<TranContext>();
            optionsBuildertran.UseSqlServer(this.GetTranConnection(facilityCode));
            return new TranContext(optionsBuildertran.Options);
        }
        #endregion
    }
}
