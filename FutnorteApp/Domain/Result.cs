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
        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ResultDate { get; set; }
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }

        // Foreign keys.
        [Required]
        public int MatchId { get; set; } = 0;

        // Navigation properties (Foreign keys as objects).
        [ForeignKey("MatchId")]
        public Match? Match { get; set; }

        // Entity Framework parameterless constructor.
        public Result() { }

        // Constructor to initialize the properties.
        public Result(int resultId, DateTime? resultDate, int? homeScore, int? awayScore, int matchId)
        {
            ResultId = resultId;
            ResultDate = resultDate;
            HomeScore = homeScore;
            AwayScore = awayScore;
            MatchId = matchId;
        }
    }
}
