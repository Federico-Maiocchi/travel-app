using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace travel_app.Models
{
    public class Stage: UserActivity
    {
       
        public int Id { get; set; }

        [DisplayName("Nome Tappa")]
        [Required(ErrorMessage = "Il campo 'Title' è obbligatorio.")]
        [StringLength(100, ErrorMessage = "Il campo 'Title' può contenere al massimo 100 caratteri.")]
        public string Title { get; set; }

        [DisplayName("Descrizione")]
        [StringLength(500, ErrorMessage = "Il campo 'Description' può contenere al massimo 500 caratteri.")]
        public string? Description { get; set; }

        [DisplayName("Immagine")]
        public string? Image{ get; set; }

        [DisplayName("Note")]
        [StringLength(300, ErrorMessage = "Il campo 'Note' può contenere al massimo 300 caratteri.")]
        public string? Note { get; set; }

        [DisplayName("Voto")]
        [Range(1, 5, ErrorMessage = "Il valore del campo 'Voto' deve essere compreso tra 1 e 5.")]
        public int? Vote { get; set; }

        [DisplayName("Data")]
        [Required(ErrorMessage = "Il campo 'Data' è obbligatorio.")]
        public DateTime Date { get; set; }

        [DisplayName("Latitudine")]
        public double Latitude { get; set; }

        [DisplayName("Longitudine")]
        public double Longitude { get; set; }

        [DisplayName("Nome Viaggio")]
        [Required(ErrorMessage = "Il campo 'TravelId' è obbligatorio.")]
        public int TravelId { get; set; }

        public Travel? Travel { get; set; }

        [DisplayName("Visitata")]
        public bool Completed { get; set; } = false;
    }
}
