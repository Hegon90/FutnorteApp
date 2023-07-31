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
        [Required]
        [Display(Name = "Fecha")]
        public DateOnly ResultDate { get; set; }
        [Required]
        public int HomeScore { get; set; } = 0;
        [Required]
        public int AwayScore { get; set; } = 0;

        // Foreign keys.
        public int RoundId { get; set; }
        [Required]
        public int HomeTeamId { get; set; } = 0;
        [Required]
        public int AwayTeamId { get; set; } = 0;

        // Navigation properties (Foreign keys as objects).
        [ForeignKey("RoundId")]
        public Round? Round { get; set; }
        [ForeignKey("HomeTeamId")]
        public Team? HomeTeam { get; set; }
        [ForeignKey("AwayTeamId")]
        public Team? AwayTeam { get; set; }


        // Entity Framework parameterless constructor.
        public Result()
        {
            ResultDate = DateOnly.FromDateTime(DateTime.Now);
        }

        // Constructor to initialize the properties.
        public Result(int resultId, DateOnly resultDate, int homeScore, int awayScore, int roundId, int homeTeamId, int awayTeamId)
        {
            ResultId = resultId;
            ResultDate = resultDate;
            HomeScore = homeScore;
            AwayScore = awayScore;
            RoundId = roundId;
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
        }

    }
}
