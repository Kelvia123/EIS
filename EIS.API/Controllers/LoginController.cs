using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using EIS.BLL;
using EIS.BOL;
using Newtonsoft.Json;

namespace EIS.API.Controllers
{
    [EnableCors("*", "*", "*")]
    public class LoginController : ApiController
    {
        private readonly EmployeeBs _employeeBs;

        public LoginController()
        {
            _employeeBs = new EmployeeBs();
        }

        [ResponseType(typeof(Employee))]
        public IHttpActionResult Post(Employee emp)
        {
            //If employee with matching Email and Password Exists
            if (_employeeBs.GetByEmail(ref emp))
            {
                return Ok(emp);
            }

            foreach (var error in _employeeBs.ErrorList)
            {
                ModelState.AddModelError("", error);
            }

            return BadRequest(ModelState);
        }

        [ResponseType(typeof(Employee))]
        [ActionName("RecoverPassword")]
        public IHttpActionResult Get(string empStr)
        {
            var emp = JsonConvert.DeserializeObject<Employee>(empStr);

            if (_employeeBs.RecoverPasswordByEmail(ref emp))
            {
                return Ok(emp);
            }

            foreach (var error in _employeeBs.ErrorList)
            {
                ModelState.AddModelError("", error);
            }

            return BadRequest(ModelState);
        }
    }
}
