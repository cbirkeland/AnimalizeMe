﻿using AnimalizeMe.Data;
using AnimalizeMe.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
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
		private readonly AnimalizeMeDbContext _context;
        private readonly IHostingEnvironment _env;

        public AnimalService(AnimalizeMeDbContext context, IHostingEnvironment env)
		{
			_context = context;
            _env = env;
        }


        const string uriBase = "https://northeurope.api.cognitive.microsoft.com/vision/v2.0/analyze";
		const string skey = "8445137c95d04e56a84c72a21f5ee696";


		public async Task<Models.AnalyzerModel.All.Rootobject> MakeAnalysisRequest(string imageFilePath)
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

				var newString = JsonConvert.DeserializeObject<Models.AnalyzerModel.All.Rootobject>(contentstring);
				//Console.WriteLine("\nResponse:\n");
				//Console.WriteLione(JsonPrettyPrint(contentstring));
				return newString;
			}
		}

        public class AnimalUrlWithScore
        {
            public string AnimalUrl { get; set; }
            public int Score { get; set; }
        }


        internal string GetAnimalUrlThatMathcesTags(string[] tags)
        {
            var list = new List<AnimalUrlWithScore>();
			// Hämta alla djur med taggar från databasen

			// Ett djur i taget => kolla hur många taggar som djuret matchar med "tags"
			foreach (var  creature in _context.Creatures.Include(x=>x.CreatureTags).ThenInclude(x=>x.Tag))
			{
				int score = 0;
				foreach (var tag in creature.CreatureTags)
				{
					if(tags.Contains(tag.Tag.Name))
					{
						score++;
					}
				}

				list.Add(new AnimalUrlWithScore
				{
					AnimalUrl = creature.ImagePath,
					Score = score
				});

			}
          

            var bestAnimal = list.OrderByDescending(x => x.Score).FirstOrDefault();

            return bestAnimal.AnimalUrl;
        }

        private byte[] GetImageAsByteArray(string name)
		{
            var imageFile = Path.Combine(_env.WebRootPath, "Images", name);
            FileStream fileStream = new FileStream(imageFile, FileMode.Open, FileAccess.Read);
			BinaryReader binaryReader = new BinaryReader(fileStream);
			return binaryReader.ReadBytes((int)fileStream.Length);
		}
		public List<string> GetTagsFromAPI()
		{
            var imagePath = Path.Combine(_env.WebRootPath, "Images");
			DirectoryInfo images = new DirectoryInfo(imagePath);//Assuming Test is your Folder
			FileInfo[] Files = images.GetFiles("*.jpg"); //Getting Text files
			var fileList = new List<string>();

           
            foreach (FileInfo file in Files)
			{
                //string ImagePath = @"C:\Users\Administrator\Desktop\AnimalizeMe\Images\" + file.Name;
                string ImagePath = file.Name; // Path.Combine(_env.ContentRootPath, "Images", file.Name);

                fileList.Add(ImagePath);
			}

			return fileList;
			//  }

			//string[] tags1 = All.Description.tags;
			//creature.CreatureTags.Add(All.Description.tags)
			// }

		}
	}
}
