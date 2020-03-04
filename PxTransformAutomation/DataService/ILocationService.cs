using PxTransform.Auto.Data.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PxTransformAutomation.DataService
{
    public interface ILocationService
    {
        /// <summary>
        /// Define the method to configure with Accretive Database
        /// </summary>
        /// <returns></returns>
        AccretiveContext GetAccretiveContext();

        /// <summary>
        /// Define the method to configure with Tran Database
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        TranContext GetTranContext(string code);
    }
}
