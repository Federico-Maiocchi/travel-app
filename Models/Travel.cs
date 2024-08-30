using System.ComponentModel.DataAnnotations;

namespace travel_app.Models
{
    public class Travel: UserActivity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(50, ErrorMessage = "Il nome non può avere più di 50 caratteri")]
        public string Title { get; set; }

        public string? Description { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La data di inizio è obbligatoria")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La data di fine è obbligatoria")]
        public DateTime EndDate { get; set; }

        public List<Stage>? Stages { get; set; }
    }
}
