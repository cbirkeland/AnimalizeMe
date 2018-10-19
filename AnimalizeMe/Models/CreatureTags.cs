using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalizeMe.Models
{
    public class CreatureTags
    {
        public int CreatureId { get; set; }
        public Creature Creature { get; set; }
        public Tag Tag { get; set; }
        public int TagId { get; set; }
    }
}
