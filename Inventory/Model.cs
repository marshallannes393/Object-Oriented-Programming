using System.Diagnostics;
using System.Xml.Linq;

namespace GroceryStore
{
    public class Item
    {
        private string category;
        private string name;
        private int price;

        public string GetCategory()
        {
            return category;
        }
        public void SetCategory(string value) { 
            category = value; 
        }

        public string GetName() {
            return name;
        }
        public void SetName(string value) { 
            name = value;
        }
        public override string ToString()
        {
            
            return category + "," + name + "," + price;
        }

        public int GetPrice() { return price; }
        public void SetPrice(int value) { price = value; }
    }

    public class CategoryRecord
    {
        private string categoryName;
        private int totalSales;

        public string GetCategoryName() { return categoryName; }
        public void SetCategoryName(string value) { categoryName = value; }

        public int GetTotalSales() { return totalSales; }
        public void SetTotalSales(int value) { totalSales = value; }
    }
    
}