using Logic.Helper_Classes;
using Logic.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Logic
{
    public class LibraryManager : INotifyPropertyChanged
    {
        private ObservableCollection<User> _users;
        private ObservableCollection<Item> _items;

        public event PropertyChangedEventHandler? PropertyChanged;

        public int Count { get => _items.Count; }
        public int TotalRentedItemsCount { get; private set; }
        public MyCashBox LibraryCashBox { get; }

        public LibraryManager()
        {
            LibraryManager? loadedData = JsonFileSerialize.LoadData();

            if (loadedData != null)
            {
                _users = loadedData.GetAllUsers();
                _items = loadedData.GetAllItems();
                LibraryCashBox = loadedData.LibraryCashBox;
                TotalRentedItemsCount = loadedData.TotalRentedItemsCount;

                // Initialize ObservableCollection for each user
                foreach (User user in _users)
                {
                    user.RentedItems = new ObservableCollection<Item>(user.RentedItems);
                }
            }

            else // first init
            {
                _users = new ObservableCollection<User>();
                AddNewUser("admin", "admin", "Admin");
                AddNewUser("2", "2", "Librarian");
                AddNewUser("1", "1", "User");
                AddNewUser("user1", "password1", "user");
                AddNewUser("user2", "password2", "user");
                AddNewUser("user3", "password3", "user");
                AddNewUser("user4", "password4", "user");
                AddNewUser("user5", "password5", "user");

                _items = new ObservableCollection<Item>();
                LibraryCashBox = new MyCashBox() { TotalCash = 1000 };
                TotalRentedItemsCount = 0;

                // Add Items for testing
                // Adding Books
                AddItem("Book", "Dune", "Science Fiction", "Penguin Random House", 30.00, 10.00, new DateTime(1965, 8, 1), "Frank Herbert");
                AddItem("Book", "To Kill a Mockingbird", "Fiction", "HarperCollins", 20.00, 5.00, new DateTime(1960, 7, 11), "Harper Lee");
                AddItem("Book", "1984", "Dystopian", "Houghton Mifflin Harcourt", 25.00, 7.00, new DateTime(1949, 6, 8), "George Orwell");
                AddItem("Book", "The Great Gatsby", "Novel", "Scribner", 15.00, 4.00, new DateTime(1925, 4, 10), "F. Scott Fitzgerald");
                AddItem("Book", "Harry Potter and the Sorcerer's Stone", "Fantasy", "Scholastic", 22.00, 6.00, new DateTime(1997, 6, 26), "J.K. Rowling");

                // Adding Magazines
                AddItem("Magazine", "National Geographic", "Science", "National Geographic Partners", 10.00, 2.00, new DateTime(2022, 7, 1), null);
                AddItem("Magazine", "Time", "News", "Time Inc.", 5.00, 1.00, new DateTime(2021, 6, 1), null);
                AddItem("Magazine", "The New Yorker", "Literature", "Condé Nast", 8.00, 1.50, new DateTime(2020, 5, 1), null);
                AddItem("Magazine", "Forbes", "Business", "Forbes Media", 6.00, 1.20, new DateTime(2019, 4, 1), null);
                AddItem("Magazine", "Rolling Stone", "Music", "Penske Media Corporation", 7.00, 1.40, new DateTime(2018, 3, 1), null);

                JsonFileSerialize.SaveData(this);
            }
        }

        // ctor for saving and loading data
        public LibraryManager(List<User> users, List<Item> items, MyCashBox libraryCashBox, int totalRented)
        {
            _users = new ObservableCollection<User>(users);
            _items = new ObservableCollection<Item>(items);
            LibraryCashBox = libraryCashBox;
            TotalRentedItemsCount = totalRented;
        }
        public ObservableCollection<Item> GetAllItems() => _items;

        public ObservableCollection<User> GetAllUsers() => _users;

        public Item? CreateItem(string priceText, string discountPriceText, string dayText, string monthText,
            string yearText, string authorText, string nameText, string genreText, string publisherText)
        {
            Item? newItem = null;

            bool isDayValid = int.TryParse(dayText, out int day) && day > 0 && day < 32;
            bool isMonthValid = int.TryParse(monthText, out int month) && month > 0 && month < 13;
            bool isYearValid = int.TryParse(yearText, out int year) && year > 0 && year < DateTime.Now.Year;
            bool isPriceValid = double.TryParse(priceText, out double price) && price >= 0;
            bool isDiscountPriceValid = double.TryParse(discountPriceText, out double discountPrice) && discountPrice >= 0 && discountPrice <= 100;

            if (isDayValid && isMonthValid && isYearValid && isPriceValid && isDiscountPriceValid)
            {

                DateTime publishDate = new(year, month, day);

                if (!string.IsNullOrEmpty(authorText)) // Item == Book
                    newItem = new Book("Book", nameText, genreText, publisherText, price, discountPrice, publishDate, authorText);

                else // Magazine
                    newItem = new Magazine("Magazine", nameText, genreText, publisherText, price, discountPrice, publishDate);
            }

            return newItem;
        }

        public void AddItem(string itemType, string name, string genre, string publisher, double price, double discountPrice, DateTime publishDate, string? author)
        {
            Item newItem;
            if (itemType.ToLower() == "book" && author != null)
                newItem = new Book(itemType, name, genre, publisher, price, discountPrice, publishDate, author);

            else // Item is of type 'Magazine'
                newItem = new Magazine(itemType, name, genre, publisher, price, discountPrice, publishDate);

            _items.Add(newItem);
            OnPropertyChanged(nameof(Count));

            JsonFileSerialize.SaveData(this);
        }

        public void AddItem(Item item)
        {
            _items.Add(item);
            LibraryCashBox.SubstractCash(100);

            OnPropertyChanged(nameof(Count));

            JsonFileSerialize.SaveData(this);
        }

        public void RemoveItem(Item item)
        {
            _items.Remove(item);
            OnPropertyChanged(nameof(Count));

            JsonFileSerialize.SaveData(this);
        }

        public Item? SearchItemByName(string itemName)
        {
            foreach (Item item in _items)
                if (item.Name.ToLower() == itemName.ToLower()) return item;

            return null;
        }

        public bool TryToRentItemByName(User user, string itemName)
        {
            Item? item = SearchItemByName(itemName);

            if (item != null && item.Renter == null) // The Item is valid for renting
            {
                item.Renter = user.Name;
                item.RentDate = DateTime.Now;
                item.ReturnDate = DateTime.Now.AddMonths(1);

                user.AddItemToRentedItems(item);

                TotalRentedItemsCount++;
                RemoveItem(item);
                OnPropertyChanged(nameof(_items));
                OnPropertyChanged(nameof(TotalRentedItemsCount));

                JsonFileSerialize.SaveData(this);

                return true;
            }
            return false;
        }

        public bool TryToReturnItemByName(User user, string itemName)
        {
            foreach (Item item in user.RentedItems)
            {
                if (item.Name.ToLower() == itemName.ToLower())
                {
                    // item.RentDate = null;
                    item.ReturnDate = DateTime.Now;
                    item.Renter = null;

                    user.RemoveItemToRentedItems(item);

                    TotalRentedItemsCount--;
                    AddItem(item);
                    OnPropertyChanged(nameof(TotalRentedItemsCount));

                    JsonFileSerialize.SaveData(this);

                    return true;
                }
            }

            return false;
        }

        public void AddNewUser(string userName, string password, string userType)
        {
            User newUser = new(userName, password, userType);
            _users.Add(newUser);

            JsonFileSerialize.SaveData(this);
        }

        public User? UserExistsCheck(string userName, string password)
        {
            foreach (User user in _users)
                if (user.Name == userName && user.Password == password) return user;
            return null;
        }

        public ObservableCollection<Item> FilterItems(string typeText, string nameText, string genreText, string authorText, string publisherText)
        {
            // Get all items initially
            ObservableCollection<Item> filteredItems = _items;

            // Apply filter criteria
            if (!typeText.Equals("Type"))
                filteredItems = new ObservableCollection<Item>(filteredItems.Where(item => item.ItemType.ToLower().Contains(typeText)));

            if (!nameText.Equals("Name"))
                filteredItems = new ObservableCollection<Item>(filteredItems.Where(item => item.Name.ToLower().Contains(nameText)));

            if (!genreText.Equals("Genre"))
                filteredItems = new ObservableCollection<Item>(filteredItems.Where(item => item.Genre.ToLower().Contains(genreText)));

            if (!authorText.Equals("Author") && !string.IsNullOrEmpty(authorText))
                filteredItems = new ObservableCollection<Item>(filteredItems.Where(item => item is Book && ((Book)item).Author.ToLower().Contains(authorText)));

            if (!publisherText.Equals("Publisher"))
                filteredItems = new ObservableCollection<Item>(filteredItems.Where(item => item.Publisher.ToLower().Contains(publisherText)));

            return filteredItems;
        }

        public bool IsValidItemEdit(string priceText, string discountPriceText, string dayText, string monthText, string yearText)
        {
            if (double.TryParse(priceText, out double price) && price >= 0 &&
                   double.TryParse(discountPriceText, out double discountPrice) && discountPrice >= 0 && discountPrice <= 100 &&
                   int.TryParse(dayText, out int day) && day > 0 && day < 32 &&
                   int.TryParse(monthText, out int month) && month > 0 && month < 13 &&
                   int.TryParse(yearText, out int year))
            {
                LibraryCashBox.SubstractCash(25);

                OnPropertyChanged(nameof(Count));
                JsonFileSerialize.SaveData(this);

                return true;
            }
            return false;
        }

        public void UpdateUserType(User user, string newUserType)
        {
            switch (newUserType)
            {
                case "user":
                    user.UserType = "User";
                    break;
                case "librarian":
                    user.UserType = "Librarian";
                    break;
                case "admin":
                    user.UserType = "Admin";
                    break;
            }

            JsonFileSerialize.SaveData(this);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        // public void AddDiscount() { }
        // public void AddToJsonFile() { }
        // public void CreateDefault() { }
        // public void EditItem() { }
        // public void GetJsonFile() { }
        // public void LateOnReturn() { }
        // public void LibrarianCheck() { }
        // public void RemoveDiscount() { }
        // public void RemoveItem() { }

    }
}