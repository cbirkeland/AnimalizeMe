using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AnimalizeMe.Services
{
    public class AnimalService
    {
        
		const string uriBase = "https://northeurope.api.cognitive.microsoft.com/vision/v2.0/analyze";
        const string skey = "8445137c95d04e56a84c72a21f5ee696";




        

		//public AnimalService(string devkey, string token)
		//{
		//	//key: 277e278c4d8c4a9402a60952299fe6cb
		//	//token: 9a5dff4f681b2b1c01f6019a2ad112469aec3af3eed58699cc7dc30a18ab6da3
		//	_devkey = devkey;
		//	_token = token;

		//}

		//public async Task<List<TrelloRoot>> GetAllBoards()
		//{
		//	string url = $" https://api.trello.com/1/members/me/boards?key=277e278c4d8c4a9402a60952299fe6cb&token=9a5dff4f681b2b1c01f6019a2ad112469aec3af3eed58699cc7dc30a18ab6da3";
		//	string result = await _httpService.Get(url);
		//	return JsonConvert.DeserializeObject<List<TrelloRoot>>(result);
		//}

		//public async Task<List<TrelloList>> GetAllListsForBoard(string id)
		//{
		//	string url = $" https://api.trello.com/1/boards/{id}/lists?key=277e278c4d8c4a9402a60952299fe6cb&token=9a5dff4f681b2b1c01f6019a2ad112469aec3af3eed58699cc7dc30a18ab6da3";
		//	string result = await _httpService.Get(url);
		//	return JsonConvert.DeserializeObject<List<TrelloList>>(result);
		//}

		//public async Task PostAPicture(string trelloListId, string name, string description)
		//{
		//	string url = $"https://api.trello.com/1/cards?name={name}&desc={description}&idList={trelloListId}&key=277e278c4d8c4a9402a60952299fe6cb&token=9a5dff4f681b2b1c01f6019a2ad112469aec3af3eed58699cc7dc30a18ab6da3";
		//	string result = await _httpService.Post(url);
		//	//postmetod utifrån id, name 
		//}

        public async Task<string> MakeAnalysisRequest(string imageFilePath)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", skey);

            string requestParameters = "visualFeatures=Categories,Description,Color&language=en";
            string uri = uriBase + "?" + requestParameters;

            HttpResponseMessage response = null;
            byte[] byteDara = GetImageAsByteArray(imageFilePath);

            using (ByteArrayContent content = new ByteArrayContent(byteDara))
            {
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, content);
                string contentstring = await response.Content.ReadAsStringAsync();

                //Console.WriteLine("\nResponse:\n");
                //Console.WriteLione(JsonPrettyPrint(contentstring));
                return contentstring;
                
            
            }


            
        }

        private static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length); 
        }
    }
}
