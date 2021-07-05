using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cozumel.Models
{
    public class Event
    {   
        [Key]
        public int ID { get; set; }
        public DateTime DateTime { get; set; }

        public string Address { get; set;}

        public int Price { get; set; }

        public string Description { get; set; }

        public int RelatedUserID { get; set; }


        public MyUser RelatedUser { get; set; }


    }
}
