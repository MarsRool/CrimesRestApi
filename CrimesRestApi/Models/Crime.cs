using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrimesRestApi.Models
{
    public class Crime
    {
        public const string DateFormat = "{0:yyyy-MM-dd H:mm}";

        [Key]
        public int Id { get; set; }
       
        [StringLength(64)]
        public string UUID { get; set; }

        [StringLength(64)]
        public string Title { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = DateFormat, ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public bool Solved { get; set; }

        public bool RequirePolice { get; set; }

        public User User { get; set; }
    }
}
