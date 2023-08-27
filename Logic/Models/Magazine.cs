namespace Logic.Models
{
    public class Magazine : Item
    {

        public Magazine() : base() { }

        public Magazine(string itemType, string name, string genre, string publisher, double price, double discountPrice, DateTime publishDate) : base(itemType, name, genre, publisher, price, discountPrice, publishDate) { }
        // public override string ToString() { return base.ToString(); }
    }
}
