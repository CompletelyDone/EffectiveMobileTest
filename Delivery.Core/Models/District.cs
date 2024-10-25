namespace Delivery.Core.Models
{
    public class District
    {
        private const int MIN_TITLE_LEN = 3;
        private const int MAX_TITLE_LEN = 100;
        private District(int id, string title)
        {
            Id = id;
            Title = title;
        }
        public int Id { get; }
        public string Title { get; }
        public static (District District, string Error) Create(int id, string title)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(title)) error = "Title can not be empty";
            if (title.Length < MIN_TITLE_LEN) error = "Title can not be less than 3 symbols";
            if (title.Length < MAX_TITLE_LEN) error = "Title can not be more than 100 symbols";

            var district = new District(id, title);

            return (district, error);
        }
           
    }
}