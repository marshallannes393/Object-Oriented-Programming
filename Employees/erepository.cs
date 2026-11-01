using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore
{
    public class EmployeeRepository
    {
        private readonly string _filename="employee.text";
        public void SaveEmployee(Employee worker)
        {
            using(StreamWriter writer=new StreamWriter(_filename,true))
            {
                writer.WriteLine(worker.ToString());
            }
        }
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            if (!File.Exists(_filename))
                return employees;
            string[] temp = File.ReadAllLines(_filename);
            for(int i=0;i<temp.Length;i++)
            {
                string name="";
                string designation = "";
                string  strsalary = "";
                string line = temp[i];
                if (string.IsNullOrEmpty(line))
                    continue;
                int comma = 0;
                for (int j=0;j<line.Length;j++)
                {
                    if (line[j]==',')
                    {
                        comma++;
                    }
                    else
                    {
                        if (comma == 0)
                        {
                            name = name + line[j];
                        }
                        if (comma == 2)
                        {
                            designation += line[j];
                        }
                        if (comma == 1)
                        {
                            strsalary += line[j];
                        }
                    }
                  

                }
                if ( name== "" || designation == "" || strsalary == "")
                    continue;
                int p = 0;
                for (int k = 0; k < strsalary.Length; k++)
                {
                    p = p * 10 + (strsalary[k] - '0');
                }
                Employee worker = new Employee();
                worker.setename(name);
                worker.setesalary(p);
                worker.setdesignation(designation);
                employees.Add(worker);

            }
            return employees;

        }
        public void RewriteAllEmployees(List<Employee> workers)
        {
            using(StreamWriter writer=new StreamWriter(_filename,false))
            {
                for(int i=0;i<workers.Count;i++)
                {
                    writer.WriteLine(workers[i].ToString());
                }
            }
        }

    }
}
