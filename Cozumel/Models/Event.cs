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
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }


        [Required]
        public DateTime Date
        {
            get; set;
        }

        [Required]
        public string Address { get; set;}

        public int Price { get; set; }

        public string RelatedUserID { get; set; }

        public MyUser RelatedUser { get; set; }


    }
}
