using System.Collections.Generic;
using AnimalizeMe.Models;

namespace AnimalizeMe.Repository
{
    public interface IAnimalRepository
    {
        void Add(Creature creature);
        List<Creature> GetAllCreatures();
    }
}