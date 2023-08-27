using LibraryApp;
using Logic;
using Logic.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace UI
{
    /// <summary>
    /// Interaction logic for UserMainPage.xaml
    /// </summary>
    public partial class UserMainPage : Page
    {
        private LibraryManager _libraryManager;
        private User? _currentUser;
        private ObservableCollection<Item>? RentedItems { get; set; }

        public UserMainPage(LibraryManager libraryManager, User? currentUser)
        {
            InitializeComponent();
            _libraryManager = libraryManager;
            _currentUser = currentUser;
            basicMainPage.DataContext = libraryManager;
            header.Text += $"{currentUser?.Name}";

            if (currentUser != null)
            {
                // Initialize your observable collection with the rented items
                RentedItems = new ObservableCollection<Item>(currentUser.RentedItems);
                // Set the ItemsSource of your DataGrid to the observable collection
                rentDataGrid.ItemsSource = RentedItems;

                // Subscribe to the CollectionChanged event
                if (_currentUser != null)
                {
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
        }

        private void BackToLoginButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.NavigateToLoginPage();
        }

        private void RentItemButton_Click(object sender, RoutedEventArgs e)
        {
            RentItemDialog rentItemDialog = new RentItemDialog();
            if (rentItemDialog.ShowDialog() == true)
            {
                string? itemName = rentItemDialog.ItemNameToRent;
                if (_currentUser != null
                    && itemName != null
                    && _libraryManager.TryToRentItemByName(_currentUser, itemName))
                {
                    UIMessages.ItemRentSuccess(itemName);
                }
                else
                {
                    ErrorMessages.ItemRentError(itemName);
                }
            }
        }

        private void ReturnItemButton_Click(object sender, RoutedEventArgs e)
        {
            ReturnItemDialog returnItemDialog = new ReturnItemDialog();
            if (returnItemDialog.ShowDialog() == true)
            {
                string? itemName = returnItemDialog.ItemNameToRent;
                if (_currentUser != null
                    && itemName != null
                    && _libraryManager.TryToReturnItemByName(_currentUser, itemName))
                {
                    UIMessages.ItemReturnSuccess(itemName);
                }
                else
                {
                    ErrorMessages.ItemReturnError(itemName);
                }
            }
        }
    }
}
