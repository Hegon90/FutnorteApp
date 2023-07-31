using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutnorteApp.Domain
{
    internal class Place
    {
        // Properties.
        [Key]
        public int PlaceId { get; set; } = 0;
        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre")]

        // Entity Framework parameterless constructor.
        public string PlaceName { get; set; } = string.Empty;

        public Place() { }

        // Constructor to initialize the properties.
        public Place(int placeId, string placeName)
        {
            PlaceId = placeId;
            PlaceName = placeName;
        }
    }
}
