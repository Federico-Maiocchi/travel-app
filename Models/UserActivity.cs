using System.ComponentModel;

namespace travel_app.Models
{
    public class UserActivity
    {
        [DisplayName("Creato Da")]
        public string? CretedById { get; set; }

        [DisplayName("Creato il")]
        public DateTime CretedOn { get; set; }

        [DisplayName("Modificato Da")]
        public string? ModifiedById { get; set; }

        [DisplayName("Modificato il")]
        public DateTime ModifiedOn { get; set; }



    }
}
