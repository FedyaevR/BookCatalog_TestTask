using SQLite;

namespace BookCatalog.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; private set; }
        [Unique, NotNull]
        public string Login { get; set; }
        [NotNull]
        public string Password { get; set; }
    }
}
