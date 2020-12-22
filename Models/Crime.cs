using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace CrimesRestApi.Models
{
    public class Crime
    {
        public const string DateFormat = "{0:yyyy-MM-dd H:mm}";
        public static readonly CrimeComparer comparer = new CrimeComparer();

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

    public class CrimeComparer : IEqualityComparer<Crime>
    {
        public bool Equals([AllowNull] Crime x, [AllowNull] Crime y)
        {
            return x.UUID == y.UUID;
        }

        public int GetHashCode([DisallowNull] Crime obj)
        {
            throw new NotImplementedException();
        }
    }
}
