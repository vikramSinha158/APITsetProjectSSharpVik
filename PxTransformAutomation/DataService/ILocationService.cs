using PxTransform.Auto.Data.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PxTransformAutomation.DataService
{
    public interface ILocationService
    {
        AccretiveContext GetAccretiveContext();

        TranContext GetTranContext(string code);
    }
}
