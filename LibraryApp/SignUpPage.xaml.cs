using LibraryApp;
using Logic;
using Logic.Models;
using System.Windows;
using System.Windows.Controls;

namespace UI
{
    /// <summary>
    /// Interaction logic for SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        private LibraryManager _libraryManger;
        public SignUpPage(LibraryManager libraryManger)
        {
            InitializeComponent();
            _libraryManger = libraryManger;
        }

        private void NewNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (newNameTextBox.Text == "New Name")
                newNameTextBox.Text = "";
        }

        private void NewNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (newNameTextBox.Text == "")
                newNameTextBox.Text = "New Name";
        }

        private void NewPasswordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (newPasswordTextBox.Text == "New Password")
                newPasswordTextBox.Text = "";
        }

        private void NewPasswordTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (newPasswordTextBox.Text == "")
                newPasswordTextBox.Text = "New Password";
        }

        private void ConfirmPasswordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (confirmPasswordTextBox.Text == "Confirm Password")
                confirmPasswordTextBox.Text = "";
        }

        private void ConfirmPasswordTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (confirmPasswordTextBox.Text == "")
                confirmPasswordTextBox.Text = "Confirm Password";
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if password field is empty
            if (string.IsNullOrEmpty(newPasswordTextBox.Text) || newPasswordTextBox.Text == "New Password")
            {
                ErrorMessages.PasswordFieldCanNotBeEmptyError();
                return;
            }

            // Check if passwords match
            if (newPasswordTextBox.Text != confirmPasswordTextBox.Text)
            {
                ErrorMessages.PasswordsNotMatchError();
                return;
            }

            // Check if user already exists
            User? existingUser = _libraryManger.UserExistsCheck(newNameTextBox.Text, newPasswordTextBox.Text);
            if (existingUser != null)
            {
                ErrorMessages.NameExistsError();
                return;
            }

            // If all checks pass, add the new user
            _libraryManger.AddNewUser(newNameTextBox.Text, newPasswordTextBox.Text, "user");

            UIMessages.SuccessfullSignUp(existingUser);


            // Navigate back to login page
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.NavigateToLoginPage();
        }

        private void BackToLoginButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.NavigateToLoginPage();
        }
    }
}
