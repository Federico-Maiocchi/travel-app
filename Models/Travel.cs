using System.ComponentModel.DataAnnotations;

namespace travel_app.Models
{
    public class Travel: UserActivity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<Stage> Stages { get; set; }
    }
}
