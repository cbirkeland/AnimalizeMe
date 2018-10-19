using AnimalizeMe.Data;
using AnimalizeMe.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalizeMe.Repository
{
    public class AnimalRepository : IAnimalRepository
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

        public List<Creature> GetAllCreatures()
        {
            return context.Creatures.Include(x => x.CreatureTags).ThenInclude(x => x.Tag).ToList();
        }

	}
}
