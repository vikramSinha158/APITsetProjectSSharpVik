﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PxTransformAutomation.Model;

namespace PxTransformAutomation.Utilities
{
    public class Utility
    {

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

        public string GetFolderPath(string appFolderName)
        {
            var folderName = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return  Path.Combine(folderName.Substring(0, folderName.LastIndexOf("\\bin")), appFolderName + "\\");
        }

        public string LoadJson(string testDataPath)
        {
            using (StreamReader r = new StreamReader(testDataPath))
            {
                var json = r.ReadToEnd();     
                return JObject.Parse(json).ToString();
 
            }
        }

        public string GetTestData(String key)
        {           
            JObject obs1 = JObject.Parse(LoadJson(GetFolderPath(TranViewDataServiceUrl["ConnectionStrings:TestDataName"]) + TranViewDataServiceUrl["ConnectionStrings:ConfigFile"]));
            return obs1[key].ToString();

        }

        public void CleanDictionary(Dictionary<string, string> ParameterList)
        {
            if (ParameterList.Count != 0)
            {
               ParameterList.Clear();
            }
        }

    


    }
}
