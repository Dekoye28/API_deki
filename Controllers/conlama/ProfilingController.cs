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
    public class ProfilingController : ControllerBase
    {
        private ProfilingRepository profilingRepository;
        public ProfilingController(ProfilingRepository profilingRepository)
        {
            this.profilingRepository = profilingRepository;
        }

        [HttpPost]
        public ActionResult Post(Profiling prof)
        {
            var hasil = profilingRepository.Insert(prof);
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
        public ActionResult Put(Profiling prof)
        {
            var hasil = profilingRepository.Update(prof);
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
        public ActionResult Delete(Profiling prof)
        {
            var hasil = profilingRepository.Delete(prof);
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
        public ActionResult Get(Profiling prof)
        {
            var hasil = profilingRepository.Get(prof.NIK);

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
            var a = profilingRepository.Get();
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

