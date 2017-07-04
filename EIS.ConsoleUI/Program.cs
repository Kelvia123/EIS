using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EIS.BLL;
using EIS.BOL;


namespace EIS.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var roleBs = new RoleBs();
            roleBs.Insert(new Role() { RoleName = "User", RoleCode = "U"});
        }
    }
}
