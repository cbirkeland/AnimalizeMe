﻿using AnimalizeMe.Models.AnalyzerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AnimalizeMe.Models.AnalyzerModel.All;

namespace AnimalizeMe.Models
{
    public class Creature 
    {
        public int Id { get; set; }

        //public Type Type { get; set; }
        public string ImagePath { get; set; }
		public List<CreatureTags> CreatureTags { get; set; }
		



	}
}
