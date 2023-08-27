using Logic.Models;
using System.Windows;

namespace UI
{
    public static class UIMessages
    {
        public static void SuccessfullSignUp(User? existingUser) => MessageBox.Show($"{existingUser?.ToString()} successfully signed up!");
        public static void SuccessfullLogin(User? user) => MessageBox.Show($"{user?.ToString()} successfully logged in!");
        public static void ItemRentSuccess(string itemName) => MessageBox.Show($"Item: {itemName} successfully rented!");
        public static void ItemReturnSuccess(string itemName) => MessageBox.Show($"Item: {itemName} successfully returned!");
        public static void ItemRemovedSuccess(string itemName) => MessageBox.Show($"Item: {itemName} successfully removed from Library List!");
        public static void ItemDiscountRemovedSuccess(string itemName) => MessageBox.Show($"The discount for the Item: {itemName} successfully removed!");
        public static void AddedNewItemMsg(string itemName) => MessageBox.Show($"Item: {itemName} successfully added!");
        public static void UpdatedItemMsg(string itemName) => MessageBox.Show($"Item: {itemName} successfully updated!");
        public static void LibrarianRentMsg(string itemName, string librarianName) => MessageBox.Show($"Item: {itemName} successfully rented by the librarian: {librarianName}");
        public static void LibrarianReturnMsg(string itemName, string librarianName) => MessageBox.Show($"Item: {itemName} successfully returned by the librarian: {librarianName}");
        public static void UserTypeUpdatedSuccessMsg(string userName, string newUserType) => MessageBox.Show($"User: {userName} successfully updated with the new Role of {newUserType}");

        /*
        public void GetReceiptMsg() { }
        */
    }
}
