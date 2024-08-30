using System.ComponentModel.DataAnnotations;

namespace travel_app.Models
{
    public class Stage: UserActivity
    {
       
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo 'Title' è obbligatorio.")]
        [StringLength(100, ErrorMessage = "Il campo 'Title' può contenere al massimo 100 caratteri.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Il campo 'Description' può contenere al massimo 500 caratteri.")]
        public string? Description { get; set; }

        public string? Image{ get; set; }

        [StringLength(300, ErrorMessage = "Il campo 'Note' può contenere al massimo 300 caratteri.")]
        public string? Note { get; set; }

        [Range(1, 5, ErrorMessage = "Il valore del campo 'Voto' deve essere compreso tra 1 e 5.")]
        public int? Vote { get; set; }

        [Required(ErrorMessage = "Il campo 'Data' è obbligatorio.")]
        public DateTime Date { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        [Required(ErrorMessage = "Il campo 'TravelId' è obbligatorio.")]
        public int TravelId { get; set; }

        public Travel Travel { get; set; }

        public bool Completed { get; set; } = false;
    }
}
