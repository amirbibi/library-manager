using LibraryApp;
using Logic;
using Logic.Models;
using System.Windows;
using System.Windows.Controls;

namespace UI
{
    /// <summary>
    /// Interaction logic for AddNewItemPage.xaml
    /// </summary>
    public partial class AddNewItemPage : Page
    {
        private LibraryManager _libraryManager;
        private User _currentUser;

        public AddNewItemPage(LibraryManager libraryManager, User currentUser)
        {
            InitializeComponent();
            _libraryManager = libraryManager;
            _currentUser = currentUser;
        }

        private void BackToLibraryButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.NavigateToLibrarianMainPage(_currentUser);
        }

        private void AddNewItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_libraryManager.LibraryCashBox.CashCheck(100))
            {
                ErrorMessages.NotEnoughCashInCashBox();
                return;
            }

            Item? newItem = _libraryManager.CreateItem(priceTextBox.Text, discountPriceTextBox.Text, dayTextBox.Text,
                monthTextBox.Text, yearTextBox.Text, authorTextBox.Text, nameTextBox.Text,
                genreTextBox.Text, publisherTextBox.Text);

            if (newItem == null)
            {
                ErrorMessages.InvalidInput();
                return;
            }

            UIMessages.AddedNewItemMsg(nameTextBox.Text);
            _libraryManager.AddItem(newItem);

            nameTextBox.Text = "";
            genreTextBox.Text = "";
            publisherTextBox.Text = "";
            priceTextBox.Text = "";
            discountPriceTextBox.Text = "";
            authorTextBox.Text = "";
            dayTextBox.Text = "Day: xx";
            monthTextBox.Text = "Month: xx";
            yearTextBox.Text = "Year: xxxx";
        }

        private void DayTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (dayTextBox.Text == "Day: xx")
                dayTextBox.Text = "";
        }

        private void DayTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (dayTextBox.Text == "")
                dayTextBox.Text = "Day: xx";
        }

        private void MonthTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (monthTextBox.Text == "Month: xx")
                monthTextBox.Text = "";
        }

        private void MonthTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (monthTextBox.Text == "")
                monthTextBox.Text = "Month: xx";
        }

        private void YearTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (yearTextBox.Text == "Year: xxxx")
                yearTextBox.Text = "";
        }

        private void YearTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (yearTextBox.Text == "")
                yearTextBox.Text = "Year: xxxx";
        }

    }
}
