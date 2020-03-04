using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PxTransformAutomation.Model;
using System.Linq;


namespace PxTransformAutomation.Utilities
{
    public class Utility
    {
        #region Methods
        /// <summary>
        /// Methos to configure Json file in project
        /// </summary>
        public IConfigurationRoot TranViewDataServiceUrl
        {
            get
            {
                var config = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json")
                 .Build();
                return config;
            }
        }

        /// <summary>
        /// Method to get application folder
        /// </summary>
        /// <param name="appFolderName"></param>
        /// <returns></returns>
        public string GetFolderPath(string appFolderName)
        {
            var folderName = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return  Path.Combine(folderName.Substring(0, folderName.LastIndexOf("\\bin")), appFolderName + "\\");
        }

        /// <summary>
        /// Method  to read json file
        /// </summary>
        /// <param name="testDataPath"></param>
        /// <returns></returns>
        public string LoadJson(string testDataPath)
        {
            using (StreamReader r = new StreamReader(testDataPath))
            {
                var json = r.ReadToEnd();     
                return JObject.Parse(json).ToString();
 
            }
        }

        /// <summary>
        /// Method to get key valye from json file test data
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetTestData(String key)
        {           
            JObject obs1 = JObject.Parse(LoadJson(GetFolderPath(TranViewDataServiceUrl["ConnectionStrings:TestDataName"]) + TranViewDataServiceUrl["ConnectionStrings:ConfigFile"]));
            return obs1[key].ToString();

        }

        /// <summary>
        /// Method to clear dictionary
        /// </summary>
        /// <param name="ParameterList"></param>
        public void CleanDictionary(Dictionary<string, string> ParameterList)
        {
            if (ParameterList.Count != 0)
            {
               ParameterList.Clear();
            }
        }
      /// <summary>
      /// Method to comapre two lists
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="list"></param>
      /// <param name="otherlist"></param>
      /// <returns></returns>
 
 
       
        public bool CompareList<T>(List<T> list, List<T> otherlist) where T : IEquatable<T>
        {
            if (list.Except(otherlist).Any())
                return false;
            if (otherlist.Except(list).Any())
                return false;
            return true;
        }
        #endregion

    }
}
