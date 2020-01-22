using RestSharp;
using RestSharp.Serialization.Json;
using Xunit;
using RestSharp.Authenticators;
using System.IO;
using Newtonsoft.Json;
using PxTransformAutomation.Model;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace PxTransformAutomation
{
    public class Tests
    {
        [Fact]
        public void readJson()
        {
            string result = string.Empty;
            var folderName = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var location = folderName.Substring(0,folderName.LastIndexOf("\\bin"));
            var testDataPath = Path.Combine(location, "TestData\\TextData.Json");

            //@"C:\ApiAutomation\PxTransformAutomation\PxTransformAutomation\TestData\TextData.json"

            using (StreamReader r = new StreamReader(testDataPath))
            {
                var json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<TextInfo>(json);
                //string cdata = items.ToString();
                //JavaScriptSerializer jss = new JavaScriptSerializer();
                //var Items = jss.Deserialize<TextInfo>(json);
                var jobj = JObject.Parse(json);
                result = jobj.ToString();
            }

            var client = new RestClient("https://stgfcapi.r1rcm.com/");


            client.Authenticator = new HttpBasicAuthenticator("Phreesia", "wRUf^6eP8ixEc^F7agU+");



            var request = new RestRequest("r1/rcm/activity/Text/{facilityCode}/v1.0/Visit/{EncounterId}/events", Method.POST);


            request.AddUrlSegment("facilityCode", "BOMC");
            request.AddUrlSegment("EncounterId", "0000026250128");

            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json", result, ParameterType.RequestBody);

            IRestResponse response = client.Execute<TextInfo>(request);

            var deserialize = new JsonDeserializer();
            var visitOutPut = deserialize.Deserialize<Dictionary<string, string>>(response);




            //request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Accept", "application/json");
            //request.AddJsonBody(result);
            //IRestResponse response = client.Execute<TextInfo>(request);

            int content = (int)response.StatusCode;

            Assert.Equal(201, (int)response.StatusCode);
            Assert.Equal("201", visitOutPut["Status"]);



        }


    }
    public class Visit1
    {
        public Visit2 Visit2 { get; set; }
    }
    public class Visit2
    {
        public string FacilityCode1 { get; set; }
        public string VisitNumber1 { get; set; }
        public string VisitType1 { get; set; }
    }
}