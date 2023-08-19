﻿using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FutnorteApp.Domain
{
    internal class Match 
    {
        // Properties.
        [Key]
        public int MatchId { get; set; } = 0;
        public DateOnly? MatchDate { get; set; }

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
        public Match(int matchId, DateOnly? matchDate, int? roundId, int homeTeamId, int awayTeamId, int? fieldId)
        {
            MatchId = matchId;
            MatchDate = matchDate;
            RoundId = roundId;
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
            FieldId = fieldId;
        }
    }
}
