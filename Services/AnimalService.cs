using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalizeMe.Services
{
    public class AnimalService
    {
		string _devkey;
		string _token;

		HttpService _httpService = new HttpService();

		public AnimalService(string devkey, string token)
		{
			//key: 277e278c4d8c4a9402a60952299fe6cb
			//token: 9a5dff4f681b2b1c01f6019a2ad112469aec3af3eed58699cc7dc30a18ab6da3
			_devkey = devkey;
			_token = token;

		}

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

		public async Task CreateAcardOnAlist(string trelloListId, string name, string description)
		{
			string url = $"https://api.trello.com/1/cards?name={name}&desc={description}&idList={trelloListId}&key=277e278c4d8c4a9402a60952299fe6cb&token=9a5dff4f681b2b1c01f6019a2ad112469aec3af3eed58699cc7dc30a18ab6da3";
			string result = await _httpService.Post(url);
			//postmetod utifrån id, name 
		}
	}
}
