using System.ComponentModel.DataAnnotations;

namespace FutnorteApp.Domain
{
    internal class Place
    {
        // Properties.
        [Key]
        public int PlaceId { get; set; } = 0;
        [Required, StringLength(100), Display(Name = "Nombre")]
        public string PlaceName { get; set; } = string.Empty;

        // Entity Framework parameterless constructor.
        public Place() { }

        // Constructor to initialize the properties.
        public Place(int placeId, string placeName)
        {
            PlaceId = placeId;
            PlaceName = placeName;
        }
    }
}
