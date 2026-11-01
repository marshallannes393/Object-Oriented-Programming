using System;
using System.Collections.Generic;

namespace GroceryStore
{
    class InventoryUI
    {
        static InventoryService _service = new InventoryService();

        public void ShowInventoryMenu()
        {
            if (!login())
                return;

            while (true)
            {
                menu();
                int choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    addStuffInInventory();
                }
                else if (choice == 2)
                {
                    viewItems();
                }
                else if (choice == 3)
                {
                    if (_service.GetSalesCount() < 2)
                    {
                        Console.WriteLine("\nNot Enough Stuff In Stock");
                        continue;
                    }

                    Console.Write("Enter The Name of The Product You Want To Buy: ");
                    string toBuy = Console.ReadLine();

                    Console.Write("Enter The Amount of Said Product You Want to Buy: ");
                    int amount = int.Parse(Console.ReadLine());

                    Console.WriteLine("\nChecking Inventory for Your Desired Product");

                    var result = _service.ProcessSale(toBuy, amount);

                    if (result.soldCount == 0)
                    {
                        Console.WriteLine("Your Desired Product is not Available");
                        continue;
                    }
                    else if (result.soldCount < amount)
                    {
                        Console.WriteLine("\nNot enough Stuff in Stock, Everything Available (" + result.soldCount + ") Will be Sold To You");
                    }
                    else
                    {
                        Console.WriteLine("\nAll requested items have been sold to you.");
                    }

                    Console.WriteLine("Product(s) Transferred to You");
                    Console.WriteLine("This is The Total Bill, Pay At The Counter - Rs." + result.totalPrice);
                    Console.WriteLine("\nThank You For Shopping Here");
                }
                else if (choice == 4)
                {
                    if (_service.GetSaleCounter() < 1)
                    {
                        Console.WriteLine("\nThere Have Been No Sales As of Now");
                        continue;
                    }

                    Console.WriteLine("\nHere You will Find the Top Performing Category in Total Sales");
                    Console.WriteLine("\nThe Category Most Sold According to Our Records is:- " + _service.GetTopPerformingCategory());
                }
                else if (choice == 5)
                {
                    Console.WriteLine("Program Exit");
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid Choice");
                }
            }
        }

        public bool login()
        {
            string username, password;
            for (int i = 1; i <= 3; i++)
            {
                Console.Write("Enter Username: ");
                username = Console.ReadLine();
                Console.Write("Enter Password: ");
                password = Console.ReadLine();

                if (username == "customer" && password == "1234")
                {
                    Console.WriteLine("Login Successful");
                    return true;
                }
                else
                {
                    Console.WriteLine("Wrong Login");
                }
            }
            Console.WriteLine("Too many attempts. Program End.");
            return false;
        }

        public void menu()
        {
            Console.WriteLine("\n---- Grocery Store Management System ----");
            Console.WriteLine("1. Add Stuff in Inventory");
            Console.WriteLine("2. Display All Items in Inventory");
            Console.WriteLine("3. Buy Something From the Store(Must Add Atleast 5 Items to Inventory)");
            Console.WriteLine("4. Top Sales");
            Console.WriteLine("5. Exit");
            Console.Write("Enter Choice: ");
        }

        public void addStuffInInventory()
        {
            Console.Write("Enter Category Name: ");
            string category = Console.ReadLine();

            Console.Write("Enter Item Name: ");
            string item = Console.ReadLine();

            Console.Write("Enter Item Price: ");
            int price = int.Parse(Console.ReadLine());

            _service.AddItemToInventory(category, item, price);
            Console.WriteLine("Item Added to Inventory Successfully!");
        }

        public void viewItems()
        {
            List<Item> items = _service.GetInventory();

            if (items.Count == 0)
            {
                Console.WriteLine("No Items Found in Inventory");
                return;
            }

            Console.WriteLine("\n--- Inventory List ---");

            List<string> printedCategories = new List<string>();

            for (int i = 0; i < items.Count; i++)
            {
                string currentCategory = items[i].GetCategory();

                bool alreadyPrinted = false;
                for (int j = 0; j < printedCategories.Count; j++)
                {
                    if (printedCategories[j] == currentCategory)
                    {
                        alreadyPrinted = true;
                        break;
                    }
                }

                if (!alreadyPrinted)
                {
                    printedCategories.Add(currentCategory);

                    int catCount = 0;
                    for (int k = 0; k < items.Count; k++)
                    {
                        if (items[k].GetCategory() == currentCategory) catCount++;
                    }

                    Console.WriteLine("[" + currentCategory + "] - Total Items: " + catCount);

                    for (int k = 0; k < items.Count; k++)
                    {
                        if (items[k].GetCategory() == currentCategory)
                        {
                            Console.WriteLine("  - " + items[k].GetName() + " : $" + items[k].GetPrice());
                        }
                    }
                    Console.WriteLine("-------------------");
                }
            }
        }
    }
}