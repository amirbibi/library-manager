using Logic.Models;
using System.Text.Json;

namespace Logic.Helper_Classes
{
    public static class JsonFileSerialize
    {
        public static void SaveData(LibraryManager libraryManager)
        {
            // Serialize
            string itemsJson = JsonSerializer.Serialize(libraryManager.GetAllItems());
            string usersJson = JsonSerializer.Serialize(libraryManager.GetAllUsers());
            string cashBoxJson = JsonSerializer.Serialize(libraryManager.LibraryCashBox);
            string totalRented = JsonSerializer.Serialize(libraryManager.TotalRentedItemsCount);

            // Write to files
            File.WriteAllText("users.json", usersJson);
            File.WriteAllText("items.json", itemsJson);
            File.WriteAllText("cashbox.json", cashBoxJson);
            File.WriteAllText("totalRented.json", totalRented);
        }

        public static LibraryManager? LoadData()
        {
            // Check if files exist
            if (!File.Exists("users.json") || !File.Exists("items.json") || !File.Exists("cashbox.json"))
                return null;

            // Read data from files
            string usersJsonFromFile = File.ReadAllText("users.json");
            string itemsJsonFromFile = File.ReadAllText("items.json");
            string cashBoxJsonFromFile = File.ReadAllText("cashbox.json");
            string totalRentedFromFile = File.ReadAllText("totalRented.json");

            // Deserialize Users manually
            var usersFromFile = new List<User>();
            var usersArray = JsonDocument.Parse(usersJsonFromFile).RootElement.EnumerateArray();

            foreach (var userElement in usersArray)
            {
                string? name = userElement.GetProperty("Name").GetString();
                string? password = userElement.GetProperty("Password").GetString();
                string? userType = userElement.GetProperty("UserType").GetString();

                if (name != null && password != null && userType != null)
                {
                    User user = new()
                    {
                        Name = name,
                        Password = password,
                        UserType = userType
                    };

                    var rentedItemsArray = userElement.GetProperty("RentedItems").EnumerateArray();

                    foreach (var itemElement in rentedItemsArray)
                    {
                        string? itemType = itemElement.GetProperty("ItemType").GetString();

                        if (itemType == "Book")
                        {
                            Book? book = JsonSerializer.Deserialize<Book>(itemElement.GetRawText());

                            if (book != null)
                                user.AddItemToRentedItems(book);
                        }
                        else if (itemType == "Magazine")
                        {
                            Magazine? magazine = JsonSerializer.Deserialize<Magazine>(itemElement.GetRawText());

                            if (magazine != null)
                                user.AddItemToRentedItems(magazine);
                        }
                    }
                    usersFromFile.Add(user);
                }
            }

            // Deserialize Items manually
            var itemsFromFile = new List<Item>();
            var itemsArray = JsonDocument.Parse(itemsJsonFromFile).RootElement.EnumerateArray();

            foreach (var itemElement in itemsArray)
            {
                string? itemType = itemElement.GetProperty("ItemType").GetString();

                if (itemType == "Book")
                {
                    Book? book = JsonSerializer.Deserialize<Book>(itemElement.GetRawText());

                    if (book != null)
                        itemsFromFile.Add(book);
                }
                else if (itemType == "Magazine") // Check if the item is a 'Magazine'
                {
                    Magazine? magazine = JsonSerializer.Deserialize<Magazine>(itemElement.GetRawText());

                    if (magazine != null)
                        itemsFromFile.Add(magazine);
                }
            }

            // Deserialize CashBox
            MyCashBox? LibraryCashBoxFromFile = JsonSerializer.Deserialize<MyCashBox>(cashBoxJsonFromFile);
            if (LibraryCashBoxFromFile == null)
                return null;

            // Deserialize Total Rented
            int? totalRented = JsonSerializer.Deserialize<int>(totalRentedFromFile);
            if (totalRented == null)
                return null;

            // Return new LibraryManager
            return new LibraryManager(usersFromFile, itemsFromFile, LibraryCashBoxFromFile, totalRented.Value);
        }


    }
}
