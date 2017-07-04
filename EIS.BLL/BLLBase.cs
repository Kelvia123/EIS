using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIS.BLL
{
    public class BLLBase
    {
        public BLLBase()
        {
            ErrorList = new List<string>();
        }
        public List<string> ErrorList { get; set; }
    }
}
