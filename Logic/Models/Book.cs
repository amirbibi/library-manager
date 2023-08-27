using System.Text;

namespace Logic.Models
{
    public class Book : Item
    {
        public string Author { get; set; }

        public Book() : base()
        {
            Author = "";
        }

        public Book(string itemType, string name, string genre, string publisher, double price, double discountPrice, DateTime publishDate, string author) : base(itemType, name, genre, publisher, price, discountPrice, publishDate)
        {
            Author = author;
        }
        public override string ToString()
        {
            StringBuilder sb = new(base.ToString());
            sb.AppendLine($"Author: {Author}");

            return sb.ToString();
        }
    }
}
