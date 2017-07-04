using EIS.BOL;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EIS.DAL
{
    public class EmployeeDb : DALBase
    {
        public IEnumerable<Emloyee> GetAll()
        {
            return _db.Employees.ToList();
        }

        public Emloyee GetById(string id)
        {
            return _db.Employees.Find(id);
        }

        public void Insert(Emloyee emloyee)
        {
            _db.Employees.Add(emloyee);
            Save();
        }

        public void Delete(string id)
        {
            var employeeInDb = _db.Employees.Find(id);
            _db.Employees.Remove(employeeInDb);
            Save();
        }

        public void Update(Emloyee emloyee)
        {
            _db.Entry(emloyee).State = EntityState.Modified;
            _db.Configuration.ValidateOnSaveEnabled = false;
            Save();
            _db.Configuration.ValidateOnSaveEnabled = true;
        }

        public Emloyee GetByEmail(string email)
        {
            return _db.Employees.FirstOrDefault(e => e.Email == email);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
