using LibraryApp;
using Logic;
using Logic.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace UI
{
    /// <summary>
    /// Interaction logic for AdminMainPage.xaml
    /// </summary>
    public partial class AdminMainPage : Page
    {
        private LibraryManager _libraryManager;
        private ObservableCollection<User>? UserList { get; set; }
        public AdminMainPage(LibraryManager libraryManager)
        {
            InitializeComponent();
            _libraryManager = libraryManager;

            UserList = new ObservableCollection<User>(libraryManager.GetAllUsers());

            // Set the ItemsSource of your DataGrid to the observable collection
            userDataGrid.ItemsSource = UserList;
        }
        private void BackToLoginButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.NavigateToLoginPage();
        }

        private void UpdateUserTypeButton_Click(object sender, RoutedEventArgs e)
        {
            User? selectedUser = userDataGrid.SelectedItem as User;

            if (selectedUser != null)
            {
                if (userRadioButton.IsChecked == true)
                    _libraryManager.UpdateUserType(selectedUser, "user");

                else if (librarianRadioButton.IsChecked == true)
                    _libraryManager.UpdateUserType(selectedUser, "librarian");

                else if (adminRadioButton.IsChecked == true)
                    _libraryManager.UpdateUserType(selectedUser, "admin");

                else
                {
                    ErrorMessages.UserNotSelectedError();
                    return;
                }

                userDataGrid.Items.Refresh();
                UIMessages.UserTypeUpdatedSuccessMsg(selectedUser.Name, selectedUser.UserType);
            }

            else
            {
                ErrorMessages.UserNotSelectedError();
            }
        }
    }
}
