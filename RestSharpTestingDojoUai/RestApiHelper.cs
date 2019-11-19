using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestingDojoUai
{
    public class RestApiHelper<T>
    {
        public RestClient _restClient;
        public RestRequest _restRequest;
        public string _baseUrl = "https://age-of-empires-2-api.herokuapp.com/";

        //Chamar o Client 
        public RestClient SetUrl(string resourseUrl)
        {
            //Combine a Urlbase + recurso que vamos utilizar 
            var url = Path.Combine(_baseUrl, resourseUrl);
            var _restClient = new RestClient(url);
            return _restClient;
        }

        //Método para retornar o request
        public RestRequest CreatePostRequest(string jsonString)
        {
            _restRequest = new RestRequest(Method.POST);
            _restRequest.AddHeader("Accept", "application/json");
            _restRequest.AddParameter("applicantion/json", jsonString, ParameterType.RequestBody);
            return _restRequest;
        }

        public RestRequest CreatePutRequest(string jsonString)
        {
            _restRequest = new RestRequest(Method.PUT);
            _restRequest.AddHeader("Accept", "application/json");
            _restRequest.AddParameter("applicantion/json", jsonString, ParameterType.RequestBody);
            return _restRequest;
        }

        public RestRequest CreateGetRequest()
        {
            _restRequest = new RestRequest(Method.GET);
            _restRequest.AddHeader("Accept", "application/json");
            return _restRequest;
        }        

        public RestRequest CreateDeleteRequest()
        {
            _restRequest = new RestRequest(Method.DELETE);
            _restRequest.AddHeader("Accept", "application/json");
            return _restRequest;
        }

        //Método para response 
        public IRestResponse GetResponse(RestClient restClient, RestRequest restRequest)
        {
            return restClient.Execute(restRequest);
        }

        public DTO GetContent<DTO>(IRestResponse response)
        {
            var content = response.Content;
            DTO deseiralizeObject = JsonConvert.DeserializeObject<DTO>(content);
            return deseiralizeObject;
        }
    }
}
