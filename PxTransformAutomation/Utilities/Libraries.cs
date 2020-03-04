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
using PxTransform.Auto.Data.Domain.Tran;

namespace PxTransformAutomation.Utilities
{
    public class Libraries
    {
        #region Methods
        /// <summary>
        /// Method to API request for GET,Post and PUT
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Method for Path Parameter
        /// </summary>
        /// <param name="request"></param>
        /// <param name="pathParams"></param>
        public void AddPathParameter(IRestRequest request, Dictionary<String, String> pathParams)
        {
            foreach (var param in pathParams)
             request.AddParameter(param.Key, param.Value, ParameterType.UrlSegment);
        }

        /// <summary>
        /// Method for Query Parameter
        /// </summary>
        /// <param name="request"></param>
        /// <param name="queryParams"></param>
        public void AddQueryParameter(IRestRequest request, Dictionary<String, String> queryParams)
        {
            foreach (var param in queryParams)
                request.AddQueryParameter(param.Key, param.Value);
        }

        /// <summary>
        /// Method for Add Header Parameter
        /// </summary>
        /// <param name="request"></param>
        /// <param name="headerParams"></param>
        public void AddHeaderParameter(IRestRequest request, Dictionary<String, String> headerParams)
        {
            foreach (var param in headerParams)
                request.AddHeader(param.Key, param.Value);
        }

        /// <summary>
        /// Method for Post request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="addBody"></param>
        public void AddPostRequestBody(IRestRequest request,string addBody)
        {
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json", addBody, ParameterType.RequestBody);

        }

        /// <summary>
        /// Method for execute reponse
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Method for Deserialize the reponse using to get the data in Dictionary
        /// </summary>
        /// <param name="restResponse"></param>
        /// <returns></returns>
        public Dictionary<string, string> DeserializeResponse(IRestResponse restResponse)
        {
            var JSONObj = new JsonDeserializer().Deserialize<Dictionary<string, string>>(restResponse);

            return JSONObj;
        }

        /// <summary>
        /// Method for Deserialize the reponse using newton soft
        /// </summary>
        /// <param name="response"></param>
        /// <param name="responseObject"></param>
        /// <returns></returns>
        public string GetResponseObject(IRestResponse response, string responseObject)
        {
            JObject obs = JObject.Parse(response.Content);
            return obs[responseObject].ToString();
        }

        /// <summary>
        /// Metghod to convert the registrationId into List
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<int> GetRegistrationIDlist(List<EligibleAccounts> data) 
        {
            List<int> regisID = new List<int>();
            foreach (var item in data)
            {
                regisID.Add(item.RegistrationID);
            }

            regisID.Sort();
            return regisID;
        }

    }
    #endregion

}
