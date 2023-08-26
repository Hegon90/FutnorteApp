using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FutnorteApp.Domain
{
    internal class Result
    {
        // Properties.
        [Key]
        public int ResultId { get; set; } = 0;
        [Required, Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ResultDate { get; set; } = DateTime.Now;
        [Required]
        public int HomeScore { get; set; } = 0;
        [Required]
        public int AwayScore { get; set; } = 0;

        // Foreign keys.
        public int RoundId { get; set; }
        [Required]
        public int MatchId { get; set; } = 0;

        // Navigation properties (Foreign keys as objects).
        [ForeignKey("RoundId")]
        public Round? Round { get; set; }
        [ForeignKey("MatchId")]
        public Match? Match { get; set; }


        // Entity Framework parameterless constructor.
        public Result() { }

        // Constructor to initialize the properties.
        public Result(int resultId, DateTime resultDate, int homeScore, int awayScore, int roundId, int matchId)
        {
            ResultId = resultId;
            ResultDate = resultDate;
            HomeScore = homeScore;
            AwayScore = awayScore;
            RoundId = roundId;
            MatchId = matchId;
        }

    }
}
