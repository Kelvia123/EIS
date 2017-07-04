using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EIS.BOL;
using EIS.DAL;

namespace EIS.BLL
{
    public class RoleBs : BLLBase
    {
        private RoleDb _roleDb;

        public RoleBs()
        {
            _roleDb = new RoleDb();
        }

        public IEnumerable<Role> GetAll()
        {
            return _roleDb.GetAll().ToList();
        }

        public Role GetById(int id)
        {
            return _roleDb.GetById(id);
        }

        public bool Insert(Role role)
        {
            if (!IsValidOnInsert(role))
                return false;

            _roleDb.Insert(role);

            return true;
        }

        public bool Update(Role role)
        {
            if (!IsValidOnUpdate(role))
                return false;

            _roleDb.Update(role);

            return true;
        }

        public void Delete(int id)
        {
            _roleDb.Delete(id);
        }

        private bool IsValidOnUpdate(Role role)
        {
            return true;
        }

        private bool IsValidOnInsert(Role role)
        {
            return true;
        }
    }
}
