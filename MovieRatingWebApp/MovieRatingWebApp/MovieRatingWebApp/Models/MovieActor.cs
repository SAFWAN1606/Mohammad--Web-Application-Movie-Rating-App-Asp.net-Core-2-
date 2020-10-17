using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRatingWebApp.Models
{
    public class MovieActor
    {
        public int ID { get; set; }
        public int ActorID { get; set; }
        public int MovieID { get; set; }
        public bool MainActor { get; set; }

        public virtual Actor Actor { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
