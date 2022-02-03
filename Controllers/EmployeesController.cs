using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
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
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeerepository;
        public EmployeesController(EmployeeRepository employeeRepository) : base(employeeRepository)
        {
            this.employeerepository = employeeRepository;
        }
        
        [HttpPost("{Register}")]
        public ActionResult<RegisterVM>Post(RegisterVM req)
        {
            var hasil = employeerepository.Register(req);
            if (hasil == 0)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest,req, message = "REGISTER GAGAL BOS" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "INSERT BERHASIL BEBS" });
            }
        }

        [Authorize(Roles = "Director, Manager")]
        [HttpGet("{RegisteredData}")]
        public ActionResult<RegisterVM> GetRegisteredData() 
        {
            var hasil = employeerepository.GetRegisteredData();
            if (hasil == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "DATA KOSONG BOS" });
            }
            else
            {
                Console.WriteLine(hasil);
                return StatusCode(200, new { status = HttpStatusCode.OK,hasil, message = "SELAMAT MENYAKSIKAN" });
            }
        }
        [HttpGet("TestCors")]
        public ActionResult TestCors() {
            return Ok("Test CORP Berhasil");
        }
        
        
    }
}
