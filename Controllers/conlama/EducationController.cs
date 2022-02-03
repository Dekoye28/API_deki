using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private EducationRepository educationRepository;
        public EducationController(EducationRepository educationRepository)
        {
            this.educationRepository = educationRepository;
        }

        [HttpPost]
        public ActionResult Post(Education ed)
        {
            var hasil = educationRepository.Insert(ed);
            if (hasil == 0)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "INSERT GAGAL" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "INSERT BERHASIL" });
            }
        }

        [HttpPut]
        public ActionResult Put(Education ed)
        {
            var hasil = educationRepository.Update(ed);
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
        public ActionResult Delete(Education ed)
        {
            var hasil = educationRepository.Delete(ed);
            if (hasil == 0)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "UPDATE GAGAL" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "DELETE BERHASIL" });
            }
        }

        [HttpGet("{NIK}")]
        public ActionResult Get(Education ed)
        {
            var hasil = educationRepository.Get(ed.EducationID);

            if (hasil == null)
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "DATA TIDAK DITEMUKAN" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, hasil, message = "DATA DITEMUKAN" });
            }
        }
        [HttpGet]
        public ActionResult Get()
        {
            var a = educationRepository.Get();
            var b = a.Count();
            if (b == 0)
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "DATA TIDAK DITEMUKAN" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, a, message = "DATA DITEMUKAN" });
            }


        }
    }
}
