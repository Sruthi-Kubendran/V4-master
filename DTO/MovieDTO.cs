using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.Models;

namespace V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.DTO
{
    public class MovieDTO
    {
        public Movie Movie { get; set; }
        public IEnumerable<int> Actors { get; set; }
    }
}
