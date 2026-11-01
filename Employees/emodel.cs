using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore
{
  
     public class Employee
        {
            private string edesignation;
            private string ename;
            private int esalary;
            public string getdesignation()
            {
                return edesignation;
            }
            public string getename()
            {
                return ename;
            }
            public int getesalary()
            {
                return esalary;
            }
            public void setename(string name)
            {
                ename = name;
            }
            public void setesalary(int salary)
            {
                esalary = salary;
            }
            public void setdesignation(string des)
            {
                edesignation = des;
            }
            public override string ToString()
            {
                return ename + "," + esalary + "," + edesignation;
            }
        }
    
}

