using API.Base;
using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountrepository;
        public IConfiguration _configuration;
        public MyContext context;
        
        public AccountsController(AccountRepository accountRepository, IConfiguration configuration, MyContext context) : base(accountRepository)
        {
            this.accountrepository = accountRepository;
            this.context = context;
            this._configuration = configuration;
        }
      

        [HttpPost("{Login}")]
        public ActionResult<RegisterVM> LoginVM(RegisterVM req)
        {
            var hasil = accountrepository.LoginVM(req);
            if (hasil == 0)
            {
                return StatusCode(404, new { status = HttpStatusCode.BadRequest, message = "DATA TIDAK DITEMUKAN BOSKUH" });
            }
            else if (hasil == 1)
            {
                var TokenSecurity = accountrepository.GetToken(req.Email);
                return StatusCode(200, new { status = HttpStatusCode.OK, TokenSecurity, message = "LOGIN SUCCESSFUL" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "PASSWORD SALAH OM" });
            }

        }
        [HttpPut("{ForgotPassword}")]
        public ActionResult<RegisterVM> ForgotPassowrd(RegisterVM req)
        {
            var hasil = accountrepository.ForgotPass(req.Email);//ALIF ALAY BOS
            if (hasil == 1)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "OTP TERKIRIM" });
            }
            else
            {
                return StatusCode(404, new {status = HttpStatusCode.NotFound, message = "DATA TIDAK DITEMUKAN" });
            }
        
        }
        [HttpGet("{ChangePassword}")]
        public ActionResult<changePass> ChangePass(changePass pass)
        {
            var hasil = accountrepository.ChangePass(pass);
            if (hasil == 1)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "PASSWORD BERHASIL DI UBAH" });
            }
            else if (hasil == 2)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "OTP EXPIRED" });//AMEL JUGA ALAY
            }
            else if (hasil == 3)
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "NEW PASSWORD DAN CONFIRM PASSWORD NOT SAME BOS" });
            }
            else if (hasil == 4)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "OTP SALAH" });
            }
            else if (hasil == 5)
            {
                return StatusCode(400, new { satatus = HttpStatusCode.BadRequest, message = "OTP SUDAH DIGUNAKAN" });
            }
            else {

                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "DATA HILANG CARI DONG" });
            }
        }
        [Authorize]
        [HttpGet("TestJWT")]
        public ActionResult TestJwt()
        {
            return Ok("Test JWT Berhasil");
        }

    }
   
}
