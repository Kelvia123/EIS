using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using EIS.BLL;
using EIS.BOL;

namespace EIS.API.Controllers
{
    [EnableCors("*","*","*")]
    public class EmployeeController : ApiController
    {
        private readonly EmployeeBs _employeeBs;

        public EmployeeController()
        {
            _employeeBs = new EmployeeBs();
        }

        [ResponseType(typeof(IEnumerable<Employee>))]
        public IHttpActionResult Get()
        {
            return Ok(_employeeBs.GetAll());
        }

        [ResponseType(typeof(Employee))]
        public IHttpActionResult Get(string id)
        {
            var employee = _employeeBs.GetById(id);

            if (employee == null) return NotFound();

            return Ok(employee);
        }

        [ResponseType(typeof(Employee))]
        public IHttpActionResult Post(Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Insert succeeded
            if (_employeeBs.Insert(employee))           
                return CreatedAtRoute("DefaultApi", new {id = employee.EmployeeId}, employee);            
            
            //Insert failed
            foreach (var error in _employeeBs.ErrorList)
            {
                ModelState.AddModelError("", error);
            }
            return BadRequest(ModelState);                                  
        }

        [ResponseType(typeof(Employee))]
        public IHttpActionResult Put(int id, Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _employeeBs.Update(employee);

            return Ok(employee);
        }

        [ResponseType(typeof(Employee))]
        public IHttpActionResult Delete(string id)
        {
            var employeeInDb = _employeeBs.GetById(id);

            if (employeeInDb == null)
                return NotFound();

            _employeeBs.Delete(id);

            return Ok(employeeInDb);
        }
    }
}
