using LibraryApp;
using Logic;
using Logic.Helper_Classes;
using Logic.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace UI
{
    /// <summary>
    /// Interaction logic for LibrarianMainPage.xaml
    /// </summary>
    public partial class LibrarianMainPage : Page
    {
        private LibraryManager _libraryManager;
        private User _currentUser;
        private ObservableCollection<Item>? RentedItems { get; set; }


        public LibrarianMainPage(LibraryManager libraryManager, User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            header.Text += $"{currentUser?.Name}";
            _libraryManager = libraryManager;
            basicMainPage.DataContext = libraryManager;

            totalCashInLibrary.DataContext = libraryManager.LibraryCashBox;

            if (currentUser != null)
            {
                // Initialize your observable collection with the rented items
                RentedItems = new ObservableCollection<Item>(currentUser.RentedItems);
                // Set the ItemsSource of your DataGrid to the observable collection
                rentDataGrid.ItemsSource = RentedItems;

                // Subscribe to the CollectionChanged event
                ((INotifyCollectionChanged)_currentUser.RentedItems).CollectionChanged += (sender, e) =>
                {
                    if (e.NewItems != null)
                    {
                        foreach (Item newItem in e.NewItems)
                        {
                            RentedItems.Add(newItem);
                        }
                    }

                    if (e.OldItems != null)
                    {
                        foreach (Item oldItem in e.OldItems)
                        {
                            RentedItems.Remove(oldItem);
                        }
                    }
                };
            }
        }

        private void WantedDiscount_GotFocus(object sender, RoutedEventArgs e)
        {
            if (wantedDiscount.Text == "Wanted Discount")
                wantedDiscount.Text = "";
        }

        private void WantedDiscount_LostFocus(object sender, RoutedEventArgs e)
        {
            if (wantedDiscount.Text == "")
                wantedDiscount.Text = "Wanted Discount";
        }

        private void RemoveItemButton_Click(object sender, RoutedEventArgs e)
        {
            Item? selectedItem = basicMainPage.itemDataGrid.SelectedItem as Item;

            if (selectedItem != null)
            {
                _libraryManager.RemoveItem(selectedItem);

                UIMessages.ItemRemovedSuccess(selectedItem.Name);
            }

            else
            {
                ErrorMessages.ItemNotSelectedError();
            }
        }

        private void RentItemButton_Click(object sender, RoutedEventArgs e)
        {
            RentItemDialog rentItemDialog = new();
            if (rentItemDialog.ShowDialog() == true)
            {
                string? itemName = rentItemDialog.ItemNameToRent;
                if (_currentUser != null
                    && itemName != null
                    && _libraryManager.TryToRentItemByName(_currentUser, itemName))
                {
                    // Add the rented item to the observable collection
                    Item? rentedItem = _libraryManager.SearchItemByName(itemName);
                    if (rentedItem != null)
                        RentedItems?.Add(rentedItem);

                    UIMessages.LibrarianRentMsg(itemName, _currentUser.Name);
                }
                else
                {
                    ErrorMessages.ItemRentError(itemName);
                }
            }
        }

        private void ReturnItemButton_Click(object sender, RoutedEventArgs e)
        {
            ReturnItemDialog returnItemDialog = new();
            if (returnItemDialog.ShowDialog() == true)
            {
                string? itemName = returnItemDialog.ItemNameToRent;
                if (_currentUser != null
                    && itemName != null
                    && _libraryManager.TryToReturnItemByName(_currentUser, itemName))
                {
                    // Remove the returned item from the observable collection
                    Item? rentedItem = _libraryManager.SearchItemByName(itemName);
                    if (rentedItem != null)
                        RentedItems?.Remove(rentedItem);

                    UIMessages.LibrarianReturnMsg(itemName, _currentUser.Name);
                }
                else
                {
                    ErrorMessages.ItemReturnError(itemName);
                }
            }
        }

        private void BackToLoginButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.NavigateToLoginPage();
        }

        private void AddNewItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_libraryManager.LibraryCashBox.CashCheck(100))
            {
                ErrorMessages.NotEnoughCashInCashBox();
            }

            else
            {
                MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
                mainWindow.NavigateToAddNewItemPage(_currentUser);
            }
        }

        private void EditItemButton_Click(object sender, RoutedEventArgs e)
        {
            Item? selectedItem = basicMainPage.itemDataGrid.SelectedItem as Item;

            if (selectedItem == null)
            {
                ErrorMessages.ItemNotSelectedError();
                return;
            }

            else if (_libraryManager.LibraryCashBox.CashCheck(25))
            {
                MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
                mainWindow.NavigateToEditItemPage(_currentUser, selectedItem);
            }

            else
            {
                ErrorMessages.NotEnoughCashInCashBox();
            }
        }

        private void RemoveDiscountButton_Click(object sender, RoutedEventArgs e)
        {
            Item? selectedItem = basicMainPage.itemDataGrid.SelectedItem as Item;

            if (selectedItem != null)
            {
                selectedItem.DiscountPrice = Converter.ConvertToPercentage(0);

                UIMessages.ItemDiscountRemovedSuccess(selectedItem.Name);
            }

            else
            {
                ErrorMessages.ItemNotSelectedError();
            }
        }

        private void AddDiscountButton_Click(object sender, RoutedEventArgs e)
        {
            Item? selectedItem = basicMainPage.itemDataGrid.SelectedItem as Item;

            if (selectedItem != null)
            {
                if (double.TryParse(wantedDiscount.Text, out double result))
                {
                    selectedItem.DiscountPrice = Converter.ConvertToPercentage(result);

                    UIMessages.ItemDiscountRemovedSuccess(selectedItem.Name);
                }
                else
                {
                    ErrorMessages.InvalidDiscountAmount();
                }
            }

            else
            {
                ErrorMessages.ItemNotSelectedError();
            }

        }
    }
}
