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
        private EmployeeDb _employeeDb;

        public EmployeeBs()
        {
            _employeeDb = new EmployeeDb();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _employeeDb.GetAll().ToList();
        }

        public Employee GetById(string id)
        {
            return _employeeDb.GetById(id);
        }

        public Employee GetByEmail(string email)
        {
            return _employeeDb.GetByEmail(email);
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

        private bool IsValidOnUpdate(Employee employee)
        {
            return true;
        }

        private bool IsValidOnInsert(Employee employee)
        {
            return true;
        }
    }
}
