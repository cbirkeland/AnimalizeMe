using AnimalizeMe.Models.AnalyzerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalizeMe.Models
{
    public class Creature : All
    {
        public int Id { get; set; }

        public Type Type { get; set; }
        public List<CreatureTags> CreatureTags { get; set; }
        public string ImagePath { get; set; }


    }
}
