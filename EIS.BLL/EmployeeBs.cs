using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EIS.DAL;
using EIS.BOL;

namespace EIS.BLL
{
    public class EmployeeBs : BLLBase
    {
        private readonly EmployeeDb _employeeDb;

        public EmployeeBs()
        {
            _employeeDb = new EmployeeDb();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _employeeDb.GetAll();
        }

        public Employee GetById(string id)
        {
            return _employeeDb.GetById(id);
        }

        public bool GetByEmail(ref Employee emp)
        {
            var empInDb = _employeeDb.GetByEmail(emp.Email);

            if (empInDb == null)
            {
                ErrorList.Add("Email Does not Exist");
            }
            else if (empInDb.Password != emp.Password)
            {
                ErrorList.Add("Invalid Password");
            }

            if (ErrorList.Count > 0)
            {
                return false;
            }

            emp = empInDb;
            return true;
        }

        public bool Insert(Employee employee)
        {
            if (!IsValidOnInsert(employee))
                return false;

            _employeeDb.Insert(employee);

            return true;
        }

        public bool Update(Employee employee)
        {
            if (!IsValidOnUpdate(employee))
                return false;

            _employeeDb.Update(employee);

            return true;
        }

        public void Delete(string id)
        {
            _employeeDb.Delete(id);
        }

        private bool IsValidOnInsert(Employee employee)
        {
            //Unique Emloyee Id Validation
            var count = GetAll().Where(e => e.EmployeeId == employee.EmployeeId).ToList().Count();
            if (count != 0)
                ErrorList.Add("This Employee Id Already Exits");
                
            //Unique Email Validation
            count = GetAll().Where(e => e.Email == employee.Email).ToList().Count();
            if (count != 0)
                ErrorList.Add("This Emial Already Exits");

            return ErrorList.Count == 0;
        }

        private bool IsValidOnUpdate(Employee employee)
        {
            //Total Exp should be greater than Relevant Exp
            if (employee.RelevantExp > employee.TotalExp)
            {
                ErrorList.Add("Total Exp should be greater than Relevant Exp");
            }

            return ErrorList.Count == 0;
        }        
    }
}
