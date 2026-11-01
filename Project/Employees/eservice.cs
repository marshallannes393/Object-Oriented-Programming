using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore
{
   
    public class eservice
    {
        private readonly EmployeeRepository _repository = new EmployeeRepository();
        
        public int getno()
        {
            return GetRoster().Count;
        }
        public bool addemployee(string name,int salary,string designation)
        {
            
            Employee worker = new Employee();
            List <Employee> employees= GetRoster();
            if(designation.ToLower()=="manager")
            {
                for(int i=0;i<employees.Count;i++)
                {
                    if (employees[i].getdesignation().ToLower()=="manager")
                    {
                
                        return false;
                    }
                }
            }
            worker.setename(name.ToLower());
            worker.setesalary(salary);
            worker.setdesignation(designation.ToLower());
            _repository.SaveEmployee(worker);
          
            return true;
        }
        public List<Employee> GetRoster()
        {
            return _repository.GetAllEmployees();
        }
        public void FireEmployee(string worker)
        {
            List<Employee> Remain = new List<Employee>();
            List<Employee> employees = GetRoster();
           
            for(int i = 0; i < employees.Count; i++)
            {
                bool found = false;
                if (employees[i].getename()==worker)
                {
                    found = true;
                }
                if(!found)
                {
                    Remain.Add(employees[i]);
                }
            }
            _repository.RewriteAllEmployees(Remain);
           
        }
    }
}
