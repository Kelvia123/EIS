﻿using EIS.BOL;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EIS.DAL
{
    public class EmployeeDb : DALBase
    {
        public IEnumerable<Employee> GetAll()
        {
            return _db.Employees.ToList();
        }

        public Employee GetById(string id)
        {
            return _db.Employees.Find(id);
        }

        public void Insert(Employee employee)
        {
            _db.Employees.Add(employee);
            Save();
        }

        public void Delete(string id)
        {
            var employeeInDb = _db.Employees.Find(id);
            _db.Employees.Remove(employeeInDb);
            Save();
        }

        public void Update(Employee employee)
        {
            _db.Entry(employee).State = EntityState.Modified;
            _db.Configuration.ValidateOnSaveEnabled = false;
            Save();
            _db.Configuration.ValidateOnSaveEnabled = true;
        }

        public Employee GetByEmail(string email)
        {
            return _db.Employees.Where(e => e.Email == email).Include(e => e.Role).FirstOrDefault();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
