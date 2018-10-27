using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DataAccess
{
    public class DA
    {
        public async static Task<List<WorkerModel>> GetData()
        {
            //await WorkerModel.SendMessage("testttttttttttttt");

            //http://btg.suprnova.cc/index.php?page=api&action=getuserworkers&api_key=6fbd4ed0d807ba94450f9cee5489a1c2653af90d3a7182741621dba573d5f527
            var client = new RestClient("http://btg.suprnova.cc");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest(@"index.php?page=api&action=getuserworkers&api_key=6fbd4ed0d807ba94450f9cee5489a1c2653af90d3a7182741621dba573d5f527", Method.GET);
            //request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
            //request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource

            // easily add HTTP Headers
            //request.AddHeader("header", "value");

            // add files to upload (works with compatible verbs)
            //request.AddFile(path);

            // execute the request
            IRestResponse response = await client.ExecuteTaskAsync(request);
            var content = response.Content;

            var fullDynamic = JsonConvert.DeserializeObject<object>(content);
            var vaelue = (fullDynamic as dynamic).getuserworkers.data;

            List<WorkerModel> list = JsonConvert.DeserializeObject<List<WorkerModel>>(vaelue.ToString());

            return list;
        }
    }
}
