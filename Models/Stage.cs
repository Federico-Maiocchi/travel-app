using System.ComponentModel.DataAnnotations;

namespace travel_app.Models
{
    public class Stage: UserActivity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Image{ get; set; }

        public string Note { get; set; }

        public int Vote { get; set; }

        public DateTime Date { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int TravelId { get; set; }

        public Travel Travel { get; set; }

        public bool Completed { get; set; }
    }
}
