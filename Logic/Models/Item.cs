using Logic.Helper_Classes;
using System.ComponentModel;
using System.Text;

namespace Logic.Models
{
    public abstract class Item : INotifyPropertyChanged
    {
        private static int _id = 0;
        private string _discountPrice = "0.0%";
        public event PropertyChangedEventHandler? PropertyChanged;

        public int ID { get; set; }
        public string ItemType { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public string? Renter { get; set; }
        public string Price { get; set; }
        public string CurrentPrice
        {
            get
            {
                double _price = Converter.ConvertBack(Price);
                double _discountPrice = Converter.ConvertBack(DiscountPrice);
                double _currentPrice = _price - (_price * _discountPrice / 100);
                return Converter.ConvertToCurrency(_currentPrice);
            }
        }
        public string DiscountPrice
        {
            get => _discountPrice;
            set
            {
                _discountPrice = value;
                OnPropertyChanged("DiscountPrice");
                OnPropertyChanged("CurrentPrice");
            }
        }

        public bool LateOnReturn { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public Item()
        {
            ID = _id++;
            ItemType = "";
            Name = "";
            Genre = "";
            Publisher = "";
            Price = "0.0";
        }

        public Item(string itemType, string name, string genre, string publisher, double price, double discountPrice, DateTime publishDate)
        {
            ID = _id++;
            ItemType = itemType;
            Name = name;
            Genre = genre;
            Publisher = publisher;
            Price = Converter.ConvertToCurrency(price);
            DiscountPrice = Converter.ConvertToPercentage(discountPrice);
            PublishDate = publishDate;
            // ReturnDate = DateTime.Now.AddMonths(1);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"Item ID: {ID}");
            sb.AppendLine($"Item Name: {Name}");
            sb.AppendLine($"Item Type: {ItemType}");
            sb.AppendLine($"Genre: {Genre}");
            sb.AppendLine($"Published by: {Publisher} on {PublishDate.ToString("d")}");
            sb.AppendLine($"Current Price: {CurrentPrice}");
            sb.AppendLine($"Discount Price: {DiscountPrice}");
            sb.AppendLine($"Original Price: {Price}");
            sb.AppendLine($"Rented by: {Renter}");
            sb.AppendLine($"Rental Date: {RentDate.ToString("d")}");
            sb.AppendLine($"Return Date: {ReturnDate.ToString("d")}");
            sb.AppendLine($"Late Return: {(LateOnReturn ? "Yes" : "No")}");

            return sb.ToString();
        }
    }
}
