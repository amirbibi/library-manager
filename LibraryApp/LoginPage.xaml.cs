using Logic;
using Logic.Models;
using System.Windows;
using System.Windows.Controls;
using UI;

namespace LibraryApp
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private LibraryManager _libraryManger;

        public LoginPage(LibraryManager libraryManger)
        {
            InitializeComponent();
            _libraryManger = libraryManger;
        }
        private void NameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text == "Name")
                nameTextBox.Text = "";
        }

        private void NameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text == "")
                nameTextBox.Text = "Name";
        }

        private void PasswordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (passwordTextBox.Text == "Password")
                passwordTextBox.Text = "";
        }

        private void PasswordTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (passwordTextBox.Text == "")
                passwordTextBox.Text = "Password";
        }


        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            User? user = _libraryManger.UserExistsCheck(nameTextBox.Text, passwordTextBox.Text);

            if (user == null)
            {
                ErrorMessages.InvalidUserNameOrPassword();
                return;
            }
            else
            {
                UIMessages.SuccessfullLogin(user);

                MainWindow mainWindow = (MainWindow)Window.GetWindow(this);

                if (user?.UserType.ToLower() == "admin")
                    mainWindow.NavigateTAdminMainPage();

                else if (user?.UserType.ToLower() == "librarian")
                    mainWindow.NavigateToLibrarianMainPage(user);

                else // Regular user
                    mainWindow.NavigateToUserMainPage(user);

            }
        }

        private void SignupButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.NavigateToSignUpPage();
        }

        private void LoginAsGuestButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.NavigateToGuestMainPage();
        }
    }
}
