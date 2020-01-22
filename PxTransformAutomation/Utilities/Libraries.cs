using System;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Deserializers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Serialization.Json;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;

namespace PxTransformAutomation.Utilities
{
    public class Libraries
    { 

        public IRestRequest GetRequest(string url, Method method)
        {
            switch (method)
            {
                case Method.GET:
                    return new RestRequest(url, Method.GET);
                case Method.POST:
                    return new RestRequest(url, Method.POST);
                case Method.PUT:
                    return new RestRequest(url, Method.PUT);
                default: 
                    return null;
            }

           }

        public void AddPathParameter(IRestRequest request, Dictionary<String, String> pathParams)
        {
            foreach (var param in pathParams)
             request.AddParameter(param.Key, param.Value, ParameterType.UrlSegment);
        }

        public void AddPostRequestBody(IRestRequest request,string addBody)
        {
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json", addBody, ParameterType.RequestBody);

        }


        public async Task<IRestResponse<T>> ExecuteAsyncRequest<T>(RestClient client, IRestRequest request) where T : class, new()
        {
            var taskCompletionSource = new TaskCompletionSource<IRestResponse<T>>();

            client.ExecuteAsync<T>(request, restResponse =>
            {
                if (restResponse.ErrorException != null)
                {
                    const string message = "Error retrieving response.";
                    throw new ApplicationException(message, restResponse.ErrorException);
                }

                taskCompletionSource.SetResult(restResponse);
            });

            return await taskCompletionSource.Task;
        }

        public Dictionary<string, string> DeserializeResponse(IRestResponse restResponse)
        {
            var JSONObj = new JsonDeserializer().Deserialize<Dictionary<string, string>>(restResponse);

            return JSONObj;
        }

        public string GetResponseObject(IRestResponse response, string responseObject)
        {
            JObject obs = JObject.Parse(response.Content);
            return obs[responseObject].ToString();
        }  

    }
}
