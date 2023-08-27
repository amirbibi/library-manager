using LibraryApp;
using Logic;
using Logic.Helper_Classes;
using Logic.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace UI
{
    /// <summary>
    /// Interaction logic for EditItemPage.xaml
    /// </summary>
    public partial class EditItemPage : Page
    {
        private LibraryManager _libraryManager;
        private User _currentUser;
        private Item _currentItem;
        public EditItemPage(LibraryManager libraryManager, User currentUser, Item currentItem)
        {
            InitializeComponent();
            _libraryManager = libraryManager;
            _currentUser = currentUser;
            _currentItem = currentItem;
        }

        private void BackToLibraryButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.NavigateToLibrarianMainPage(_currentUser);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            double _price = Converter.ConvertBack(_currentItem.Price);
            double _discountPrice = Converter.ConvertBack(_currentItem.DiscountPrice);

            nameTextBox.Text = _currentItem.Name;
            genreTextBox.Text = _currentItem.Genre;
            publisherTextBox.Text = _currentItem.Publisher;
            priceTextBox.Text = _price.ToString();
            discountPriceTextBox.Text = _discountPrice.ToString();
            dayTextBox.Text = _currentItem.PublishDate.Day.ToString();
            monthTextBox.Text = _currentItem.PublishDate.Month.ToString();
            yearTextBox.Text = _currentItem.PublishDate.Year.ToString();

            if (_currentItem is Book book) // Item is Book
                authorTextBox.Text = book.Author;
            else // Item is Magazine
                authorTextBox.Text = "";
        }

        private void EditItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_libraryManager.LibraryCashBox.CashCheck(25))
            {
                ErrorMessages.NotEnoughCashInCashBox();
                return;
            }

            // Get and validate the values from the TextBoxes
            if (_libraryManager.IsValidItemEdit(priceTextBox.Text, discountPriceTextBox.Text, dayTextBox.Text, monthTextBox.Text, yearTextBox.Text))
            {
                // Update the properties of the currentItem
                _currentItem.Name = nameTextBox.Text;
                _currentItem.Genre = genreTextBox.Text;
                _currentItem.Publisher = publisherTextBox.Text;
                _currentItem.Price = Converter.ConvertToCurrency(double.Parse(priceTextBox.Text));
                _currentItem.DiscountPrice = Converter.ConvertToPercentage(double.Parse(discountPriceTextBox.Text));
                _currentItem.PublishDate = new DateTime(int.Parse(yearTextBox.Text), int.Parse(monthTextBox.Text), int.Parse(dayTextBox.Text));

                if (_currentItem is Book book)
                    book.Author = authorTextBox.Text;

                UIMessages.UpdatedItemMsg(_currentItem.Name);
            }
            else
            {
                // If the inputs are not valid, show an error message
                ErrorMessages.InvalidInput();
            }
        }
    }
}
