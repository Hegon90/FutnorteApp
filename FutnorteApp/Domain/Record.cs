using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace FutnorteApp.Domain
{
    internal class Record //record-id, team-id, matches-played, goals-for, goals-against, team-wins, team-draws, team-losses, team-points.
    {
        // Properties.
        [Key]
        public int RecordId { get; set; } = 0;
        [Required]
        [Display(Name = "PJ")]
        public int GamesPlayed { get; set; } = 0;
        [Required]
        [Display(Name = "GF")]
        public int GoalsFor { get; set; } = 0;
        [Required]
        [Display(Name = "GC")]
        public int GoalsAgainst { get; set; } = 0;
        public int TeamWins { get; set; } = 0;
        public int TeamDraws { get; set; } = 0;
        public int TeamLosses { get; set; } = 0;

        // Foreign keys.
        public int TeamId { get; set; } = 0;
    }
}
