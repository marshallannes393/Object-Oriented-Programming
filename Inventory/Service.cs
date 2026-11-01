using System;
using System.Collections.Generic;

namespace GroceryStore
{
    public class InventoryService
    {
        private InventoryRepository _repository = new InventoryRepository();

        private int salesCount = 0;
        private int saleCounter = 0;
        private List<CategoryRecord> _categorySales = new List<CategoryRecord>();

        public int GetSalesCount() { return salesCount; }
        public int GetSaleCounter() { return saleCounter; }

        public void AddItemToInventory(string category, string name, int price)
        {
            salesCount++;

            Item newItem = new Item();
            newItem.SetCategory(category.ToLower());
            newItem.SetName(name.ToLower());
            newItem.SetPrice(price);

            _repository.SaveItem(newItem);
        }

        public List<Item> GetInventory()
        {
            return _repository.GetAllItems();
        }

        public (int soldCount, int totalPrice, string categorySold) ProcessSale(string toBuy, int amount)
        {
            List<Item> items = GetInventory();
            int soldCount = 0;
            int totalPrice = 0;
            string categorySold = "";
            List<Item> itemsToKeep = new List<Item>();
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].GetName() == toBuy && soldCount < amount)
                {
                    categorySold = items[i].GetCategory();
                    totalPrice += items[i].GetPrice();
                    soldCount++;
                }
                else
                {
                    itemsToKeep.Add(items[i]);
                }
            }

            if (soldCount > 0)
            {
                saleCounter++;
                _repository.RewriteAllItems(itemsToKeep);
                UpdateCategorySales(categorySold, soldCount);
            }

            return (soldCount, totalPrice, categorySold);
        }

        private void UpdateCategorySales(string category, int amountSold)
        {
            bool found = false;
            for (int i = 0; i < _categorySales.Count; i++)
            {
                if (_categorySales[i].GetCategoryName() == category)
                {
                    _categorySales[i].SetTotalSales(_categorySales[i].GetTotalSales() + amountSold);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                CategoryRecord newRecord = new CategoryRecord();
                newRecord.SetCategoryName(category);
                newRecord.SetTotalSales(amountSold);
                _categorySales.Add(newRecord);
            }
        }

        public string GetTopPerformingCategory()
        {
            string res = "\nnothing has been sold yet\n";
            if (_categorySales.Count == 0) return res;

            for (int i = 0; i < _categorySales.Count; i++)
            {
                for (int j = 0; j < _categorySales.Count - i - 1; j++)
                {
                    if (_categorySales[j].GetTotalSales() > _categorySales[j + 1].GetTotalSales())
                    {
                       
                        (_categorySales[j], _categorySales[j + 1]) = (_categorySales[j + 1], _categorySales[j]);
                    }
                }
            }

            CategoryRecord top = _categorySales[_categorySales.Count - 1];
            return top.GetCategoryName() + " -with - " + top.GetTotalSales() + " -sales";
        }
    }
}