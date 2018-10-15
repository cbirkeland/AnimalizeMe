using AnimalizeMe.Data;
using AnimalizeMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalizeMe.Repository
{
    public class AnimalRepository
    {
		private readonly AnimalizeMeDbContext context;

		public AnimalRepository(AnimalizeMeDbContext context)
		{
			this.context = context;
		}

		public void Add(Creature creature)
		{
			context.Add(creature);
			context.SaveChanges();
		}

	}
}
