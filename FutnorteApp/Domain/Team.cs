using System.ComponentModel.DataAnnotations;

namespace FutnorteApp.Domain
{
    internal class Team
    {
        // Properties.
        [Key]
        public int TeamId { get; set; } = 0;
        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre Equipo")]
        public string TeamName { get; set; } = string.Empty;

        // Entity Framework parameterless constructor.
        public Team()
        {
        }

        // Constructor to initialize the properties.
        public Team(int teamId, string teamName)
        {
            TeamId = teamId;
            TeamName = teamName;
        }

    }
}
