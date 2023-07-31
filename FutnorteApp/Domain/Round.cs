using System;
using System.ComponentModel.DataAnnotations;

namespace FutnorteApp.Domain
{
    internal class Round
    {
        // Properties.
        [Key]
        public int RoundId { get; set; } = 0;
        [StringLength(30)]
        [Display(Name = "Nombre")]
        public string? RoundName { get; set; }
        public DateOnly RoundDate { get; set; }

        // Entity Framework parameterless constructor.
        public Round()
        {
        }

        // Constructor to initialize the properties.
        public Round(int roundId, string roundName, DateOnly roundDate)
        {
            RoundId = roundId;
            RoundName = roundName;
            RoundDate = roundDate;
        }

    }
}
