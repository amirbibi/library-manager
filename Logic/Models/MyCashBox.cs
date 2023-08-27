using System.ComponentModel;

namespace Logic.Models
{
    public class MyCashBox : INotifyPropertyChanged
    {
        //private double _addItemCost;
        //private double _editItemCost;

        public double TotalCash { get; set; }

        public MyCashBox() { }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddCash(double addedCash)
        {
            double totalCash = Math.Round(TotalCash + addedCash, 6);
            TotalCash = totalCash;
            OnPropertyChanged(nameof(TotalCash));
        }

        public void SubstractCash(double substractedCash)
        {
            double totalCash = Math.Round(TotalCash - substractedCash, 6);
            TotalCash = totalCash;
            OnPropertyChanged(nameof(TotalCash));
        }

        public bool CashCheck(int price)
        {
            return (TotalCash >= price);
        }

        /*
        public void AddCost() { }
        public void EditCost() { }
        public void RentItem() { }
        */
        public override string ToString() { return TotalCash.ToString(); }
    }
}
