using System;

namespace GroceryStore
{
    class Driver
    {
        static void Main()
        {
            EmployeeUI eui = new EmployeeUI();
            InventoryUI iui = new InventoryUI();
            while (true)
            {
                Console.WriteLine("\n=====================================");
                Console.WriteLine("   GROCERY STORE MANAGEMENT SYSTEM");
                Console.WriteLine("=====================================");
                Console.WriteLine("1. Inventory Management");
                Console.WriteLine("2. HR & Employee Management");
                Console.WriteLine("3. Exit Application");
                Console.Write("Select a department: ");

                int input = int.Parse(Console.ReadLine());


                if (input == 1)
                {

                    iui.ShowInventoryMenu();
                }
                else if (input == 2)
                {

                    eui.ShowEmployeeMenu();
                }
                else if (input == 3)
                {
                    Console.WriteLine("Shutting down system. Goodbye!");
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please select 1, 2, or 3.");
                }


            }
        }
    }
}

    

        
    

