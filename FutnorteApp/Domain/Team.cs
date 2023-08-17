using System.ComponentModel.DataAnnotations;

namespace FutnorteApp.Domain
{
    internal class Team
    {
        // Properties.
        [Key]
        public int TeamId { get; set; } = 0;
        [Required, StringLength(50), Display(Name = "Nombre Equipo")]
        public string TeamName { get; set; } = string.Empty;
        [StringLength(3), Display(Name = "Grupo")]
        public string TeamGroup { get; set; } = "N/A";
        public string? TeamColor { get; set; }
        public string? TeamManager { get; set; }
        public string? TeamPhoneNumber { get; set; }

        // Entity Framework parameterless constructor.
        public Team() { }

        // Constructor to initialize the properties.
        public Team(int teamId, string teamName, string teamGroup, string? teamColor, string? teamManager, string? teamPhoneNumber)
        {
            TeamId = teamId;
            TeamName = teamName;
            TeamGroup = teamGroup;
            TeamColor = teamColor;
            TeamManager = teamManager;
            TeamPhoneNumber = teamPhoneNumber;  
        }

    }
}
