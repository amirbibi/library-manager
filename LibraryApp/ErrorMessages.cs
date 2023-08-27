using System.Windows;

namespace UI
{
    public static class ErrorMessages
    {
        public static void NameExistsError() => MessageBox.Show("Error: The name already exists");
        public static void PasswordsNotMatchError() => MessageBox.Show("Error: The passwords entered do not match");
        public static void PasswordFieldCanNotBeEmptyError() => MessageBox.Show("Error: Password field cannot be left empty");
        public static void InvalidUserNameOrPassword() => MessageBox.Show("Error: Invalid username or password");
        public static void ItemDoesNotExistsError(string itemName) => MessageBox.Show($"Error: Item with the name: '{itemName}' does not exists");
        public static void ItemRentError(string? itemName) => MessageBox.Show($"Error: Item: '{itemName}' is unavailiable for rent");
        public static void ItemReturnError(string? itemName) => MessageBox.Show($"Error: Item: '{itemName}' is unavailiable to return because it is not in your rent list");
        public static void ItemNotSelectedError() => MessageBox.Show("Error: No Item has been selected in the library list");
        public static void UserNotSelectedError() => MessageBox.Show("Error: No User has been selected in the user list");
        public static void UserTypeNotSelectedError() => MessageBox.Show("Error: No User Type has been selected in the radio selection");
        public static void InvalidInput() => MessageBox.Show("Error: One or more of the inputs are not valid");
        public static void InvalidDiscountAmount() => MessageBox.Show("Error: Invalid wanted discount amount");
        public static void NotEnoughCashInCashBox() => MessageBox.Show("Error: Not enough cash in the library's CashBox");

        /*
        public static void FutureDateError() { }
        public static void MagazineAuthorError() { }
        public static void NegativeNumberError() { }
        public static void NullOrEmptyError() { }
        public static void UnknownIDError() { }
        public static void WrongUserError() { }
        */
    }
}
