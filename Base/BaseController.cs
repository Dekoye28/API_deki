using API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;
        public BaseController(Repository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public ActionResult<Entity> Get() {

            var result = repository.Get();
            return Ok(result);
        }
        [HttpPost]
        public ActionResult<Entity> Post(Entity entity)
        {
            var hasil = repository.Insert(entity);
            if (hasil == 0)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "INSERT GAGAL" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK,entity, message = "INSERT BERHASIL" });
            }
        }

        [HttpPut]
        public ActionResult<Entity> Put(Entity entity)
        {
            var hasil = repository.Update(entity);
            if (hasil == 0)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "UPDATE GAGAL" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "UPDATE BERHASIL" });
            }
        }

        [HttpDelete]
        public ActionResult<Entity> Delete(Key key)
        {
            
            var hasil = repository.Delete(key);
            Console.WriteLine(hasil);
            if (hasil == 0)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest,key,message = "DELETE GAGAL" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "DELETE BERHASIL" });
            }
        }

    }
}
