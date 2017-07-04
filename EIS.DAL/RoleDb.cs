using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EIS.BOL;

namespace EIS.DAL
{
    public class RoleDb : DALBase
    {      
        public IEnumerable<Role> GetAll()
        {
            return _db.Roles.ToList();
        }

        public Role GetById(int id)
        {
            return _db.Roles.Find(id);
        }

        public void Insert(Role role)
        {
            _db.Roles.Add(role);
            Save();
        }

        public void Delete(int id)
        {
            var roleInDb = _db.Roles.Find(id);
            _db.Roles.Remove(roleInDb);
            Save();
        }

        public void Update(Role role)
        {
            _db.Entry(role).State = EntityState.Modified;
            _db.Configuration.ValidateOnSaveEnabled = false;
            Save();
            _db.Configuration.ValidateOnSaveEnabled = true;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
