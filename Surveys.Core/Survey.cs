using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveys.Core
{
    public class Survey
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string FavoriteTeam { get; set; }
        public override string ToString()
        {
            return $"{Name} | {BirthDate} | {FavoriteTeam}";
        }
    }
}
