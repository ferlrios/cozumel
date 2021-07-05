using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cozumel.Models
{
    public class MyUser : IdentityUser
    {
        public byte[] ProfileImg { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Description { get; set; }

        public string InstagramURL { get; set; }

        public string YoutubeURL { get; set; }

        public string SpotifyURL { get; set; }

        public string Address { get; set; }


    }
}
