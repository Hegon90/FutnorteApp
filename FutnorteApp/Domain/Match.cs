using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using FutnorteApp.Domain;

internal class Match
{
    // Properties.
    [Key]
    public int MatchId { get; set; } = 0;
    [Column(TypeName = "date")]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? MatchDate { get; set; }
    public string? MatchTime { get; set; }

    // Foreign keys.
    public int? RoundId { get; set; }
    [Required]
    public int HomeTeamId { get; set; } = 0;
    [Required]
    public int AwayTeamId { get; set; } = 0;
    public int? FieldId { get; set; }

    // Navigation properties
    [ForeignKey("RoundId")]
    public Round? Round { get; set; }

    [ForeignKey("HomeTeamId")]
    public Team? HomeTeam { get; set; }

    [ForeignKey("AwayTeamId")]
    public Team? AwayTeam { get; set; }

    [ForeignKey("FieldId")]
    public Field? Field { get; set; }

    // Entity Framework parameterless constructor.
    public Match() { }

    // Constructor to initialize the properties.
    public Match(int matchId, DateTime? matchDate, string? matchTime, int? roundId, int homeTeamId, int awayTeamId, int? fieldId)
    {
        MatchId = matchId;
        MatchDate = matchDate;
        MatchTime = matchTime;
        RoundId = roundId;
        HomeTeamId = homeTeamId;
        AwayTeamId = awayTeamId;
        FieldId = fieldId;
    }
}
