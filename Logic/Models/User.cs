using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Logic.Models
{
    public class User : INotifyPropertyChanged
    {
        private ObservableCollection<Item> _rentedItems = new ObservableCollection<Item>();
        public string Name { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public int RentedItemsCount { get => RentedItems.Count; }
        public ObservableCollection<Item> RentedItems
        {
            get
            {
                return _rentedItems;
            }
            set
            {
                if (_rentedItems != value)
                {
                    _rentedItems = value;
                    OnPropertyChanged(nameof(RentedItems));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public User()
        {
            Name = "";
            Password = "";
            UserType = "";
        }

        public User(string name, string password, string userType)
        {
            Name = name;
            Password = password;
            UserType = userType;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void AddItemToRentedItems(Item item) => RentedItems.Add(item);
        public void RemoveItemToRentedItems(Item item) => RentedItems.Remove(item);

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"User Name: {Name}");
            // sb.AppendLine($"User Password: {Password}");
            sb.AppendLine($"User Type: {UserType}");

            return sb.ToString();
        }
    }
}
