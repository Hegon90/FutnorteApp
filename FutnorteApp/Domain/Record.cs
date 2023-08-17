using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace FutnorteApp.Domain
{
    internal class Record 
    {
        // Properties.
        [Key]
        public int RecordId { get; set; } = 0;
        [Required, Display(Name = "PJ")]
        public int GamesPlayed { get; set; } = 0;
        [Required, Display(Name = "GF")]
        public int GoalsFor { get; set; } = 0;
        [Required, Display(Name = "GC")]
        public int GoalsAgainst { get; set; } = 0;
        [Required, Display(Name = "PG")]
        public int TeamWins { get; set; } = 0;
        [Required, Display(Name = "PE")]
        public int TeamDraws { get; set; } = 0;
        [Required, Display(Name = "PP")]
        public int TeamLosses { get; set; } = 0;
        [Required, Display(Name = "PTS")]
        public int TeamPoints { get; set; } = 0;

        // Foreign keys.
        public int TeamId { get; set; } = 0;

        // Entity Framework parameterless constructor.
        public Record() { }

        // Constructor to initialize the properties.
        public Record(int recordId, int gamesPlayed, int goalsFor, int goalsAgainst, int teamwins, int teamDraws, int teamLosses, int teamPoints)
        {
            RecordId = recordId;
            GamesPlayed = gamesPlayed;
            GoalsFor = goalsFor;
            GoalsAgainst = goalsAgainst;
            TeamWins = teamwins;
            TeamDraws = teamDraws;
            TeamLosses = teamLosses;
            TeamPoints = teamPoints;
        }
    }
}
