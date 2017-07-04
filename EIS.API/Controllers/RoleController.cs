using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using EIS.BLL;
using EIS.BOL;

namespace EIS.API.Controllers
{
    public class RoleController : ApiController
    {
        private readonly RoleBs _roleBs;

        public RoleController()
        {
            _roleBs = new RoleBs();
        }

        [ResponseType(typeof(IEnumerable<Role>))]
        public IHttpActionResult Get()
        {
            return Ok(_roleBs.GetAll());
        }

        [ResponseType(typeof(Role))]
        public IHttpActionResult Get(int id)
        {
            var role = _roleBs.GetById(id);

            if (role == null)
                return NotFound();

            return Ok(role);
        }

        [ResponseType(typeof(Role))]
        public IHttpActionResult Post(Role role)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _roleBs.Insert(role);

            return CreatedAtRoute("DefaultApi", new { id = role.RoleId}, role);
        }

        [ResponseType(typeof(Role))]
        public IHttpActionResult Put(int id, Role role)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _roleBs.Update(role);

            return Ok(role);
        }

        [ResponseType(typeof(Role))]
        public IHttpActionResult Delete(int id)
        {
            var roleInDb = _roleBs.GetById(id);

            if (roleInDb == null)
                return NotFound();

            _roleBs.Delete(id);

            return Ok(roleInDb);
        }
    }
}
