using System;
using System.Collections.Generic;

namespace GroceryStore
{
    public class EmployeeUI
    {
      
        private eservice _service = new eservice();

        public void ShowEmployeeMenu()
        {
            if(!login())
            {
                return;
            }
            while (true)
            {
                Console.WriteLine("\n--- Employee Management (HR) ---");
                Console.WriteLine("1. Hire (Add) New Employee");
                Console.WriteLine("2. View Employee Roster");
                Console.WriteLine("3. Fire Employee");
                Console.WriteLine("4. Return to Main Menu");
                Console.Write("Enter Choice: ");

                int choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    hireEmployee();
                }
                else if (choice == 2)
                {
                    viewEmployees();
                }
                else if (choice == 3)
                {
                    fireEmployee();
                }
                else if (choice == 4)
                {
                    
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid Choice.");
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

                if (username == "manager" && password == "1234")
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

        private void hireEmployee()
        {
            Console.Write("Enter Employee Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Salary: ");
            int salary = int.Parse(Console.ReadLine());

            Console.Write("Enter Designation (e.g., Manager, Cashier): ");
            string designation = Console.ReadLine();

            if (_service.addemployee(name, salary, designation))
            {
                Console.WriteLine("Employee successfully hired!");
            }
            else
            {
                Console.WriteLine("There can only ever be one manager");
            }
        }

        private void viewEmployees()
        {
            List<Employee> roster = _service.GetRoster();

            if (roster.Count == 0)
            {
                Console.WriteLine("\nNo employees found. The roster is empty.");
                return;
            }

            Console.WriteLine("\n--- Current Roster ---");
            for (int i = 0; i < roster.Count; i++)
            {
           
                Console.WriteLine("- " + roster[i].getename() + " | Designation: " + roster[i].getdesignation() + " | Salary: $" + roster[i].getesalary());
            }
            Console.WriteLine("Total Employees: " + _service.getno());
        }

        private void fireEmployee()
        {
            if (_service.getno() == 0)
            {
                Console.WriteLine("\nNo employees to fire.");
                return;
            }

            Console.Write("\nEnter the exact name of the employee to fire: ");
            string targetName = Console.ReadLine();
            List<Employee> employees = _service.GetRoster();
            Employee targetEmployee = null;
            for (int i = 0; i < employees.Count; i++)
            {
                if (employees[i].getename() == targetName)
                {
                    targetEmployee = employees[i];
                    break;
                }
            }
            if (targetEmployee != null)
            {
                _service.FireEmployee(targetName);
                Console.WriteLine("Employee has been terminated.");
            }
            else
            {
                Console.WriteLine("Employee not found in the roster.");
            }
        }
    }
}