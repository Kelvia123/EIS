using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIS.DAL
{
    public class DALBase
    {
        protected EISDBContext _db;

        public DALBase()
        {
            _db = new EISDBContext();
        }
    }
}
