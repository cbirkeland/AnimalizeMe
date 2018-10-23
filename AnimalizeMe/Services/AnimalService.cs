using AnimalizeMe.Data;
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
using AnimalizeMe.Repository;

namespace AnimalizeMe.Services
{
	public class AnimalService
	{
        private readonly IHostingEnvironment _env;

        public AnimalService(IHostingEnvironment env)
		{
            _env = env;
        }


        const string uriBase = "https://northeurope.api.cognitive.microsoft.com/vision/v2.0/analyze";
		const string skey = "f5b58443d57248b9a51d8210df3a4e30";


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
				
				return newString;
			}
		}

        public class AnimalUrlWithScore
        {
            public string AnimalUrl { get; set; }
            public int Score { get; set; }
        }

         



        public string GetAnimalUrlThatMatchesTags(string[] tags, List<Creature> allCreatures)
        {
            var list = new List<AnimalUrlWithScore>();

            // Hämta alla djur med taggar från databasen
            //Ett djur i taget => kolla hur många taggar som djuret matchar med "tags"
			foreach (var  creature in allCreatures)
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
          
            var bestAnimal = list.Where(x=>x.Score>0).OrderByDescending(x => x.Score).FirstOrDefault();

            return bestAnimal?.AnimalUrl;
        }

        private byte[] GetImageAsByteArray(string name)
		{
            var imageFile = Path.Combine(_env.WebRootPath, "Images", name);
            FileStream fileStream = new FileStream(imageFile, FileMode.Open, FileAccess.Read);
			BinaryReader binaryReader = new BinaryReader(fileStream);
			return binaryReader.ReadBytes((int)fileStream.Length);
		}
		public List<string> CreatesFileListOfImagePathFromFolder()
		{
            var imagePath = Path.Combine(_env.WebRootPath, "Images");
			DirectoryInfo images = new DirectoryInfo(imagePath);
			FileInfo[] Files = images.GetFiles("*.jpg"); 
			var fileList = new List<string>();

           
            foreach (FileInfo file in Files)
			{
               
                string ImagePath = file.Name; 

                fileList.Add(ImagePath);
			}

			return fileList;
		

		}
	}
}
