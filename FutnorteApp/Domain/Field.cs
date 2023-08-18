using System.ComponentModel.DataAnnotations;

namespace FutnorteApp.Domain
{
    internal class Field
    {
        // Properties.
        [Key]
        public int FieldId { get; set; } = 0;
        [Required, StringLength(100), Display(Name = "Nombre")]
        public string FieldName { get; set; } = string.Empty;

        // Entity Framework parameterless constructor.
        public Field() { }

        // Constructor to initialize the properties.
        public Field(int fieldId, string fieldName)
        {
            FieldId = fieldId;
            FieldName = fieldName;
        }
    }
}
