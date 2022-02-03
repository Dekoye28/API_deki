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
    public class AcoountRolesController : BaseController<AccountRole, AccountRoleRepository, string>
    {
        private readonly AccountRoleRepository accountRoleRepository;
        public AcoountRolesController(AccountRoleRepository accountRoleRepository) : base(accountRoleRepository) {
            this.accountRoleRepository = accountRoleRepository;
        }

        [Authorize(Roles = "Director")]
        [HttpPut("Assign")]
        public ActionResult AssignManager(AccountRole ar) {
            var hasil = accountRoleRepository.AssignManager(ar.NIK);
            if (hasil == 1)
            {
                return StatusCode(200,new { HttpStatusCode.OK, message = "PROMOSI JABATAN BERHASIL DILAKUKAN" });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "DATA TIDAK DITEMUKAN" });
            }
        }
    }
}
