using Logic;
using Logic.Models;
using System.Windows;
using UI;

namespace LibraryApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LibraryManager _libraryManager;
        public MainWindow()
        {
            InitializeComponent();
            _libraryManager = new LibraryManager();

            mainFrame.Navigate(new LoginPage(_libraryManager));
        }
        public void NavigateToSignUpPage()
        {
            mainFrame.Navigate(new SignUpPage(_libraryManager));
        }

        public void NavigateToLoginPage()
        {
            mainFrame.Navigate(new LoginPage(_libraryManager));
        }

        public void NavigateToGuestMainPage()
        {
            mainFrame.Navigate(new GuestMainPage(_libraryManager));
        }

        public void NavigateToUserMainPage(User? currentUser)
        {
            mainFrame.Navigate(new UserMainPage(_libraryManager, currentUser));
        }

        public void NavigateToLibrarianMainPage(User currentUser)
        {
            mainFrame.Navigate(new LibrarianMainPage(_libraryManager, currentUser));
        }

        public void NavigateToAddNewItemPage(User currentUser)
        {
            mainFrame.Navigate(new AddNewItemPage(_libraryManager, currentUser));
        }

        public void NavigateToEditItemPage(User currentUser, Item currentItem)
        {
            mainFrame.Navigate(new EditItemPage(_libraryManager, currentUser, currentItem));
        }

        public void NavigateTAdminMainPage()
        {
            mainFrame.Navigate(new AdminMainPage(_libraryManager));
        }
    }
}
