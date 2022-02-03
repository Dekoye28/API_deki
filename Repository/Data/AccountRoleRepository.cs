using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRoleRepository : GeneralRepositoty<MyContext, AccountRole, string>
    {
        public readonly MyContext context;
        public AccountRoleRepository(MyContext context) : base(context)
        {
            this.context = context;
        }
        public int AssignManager(string nik) {
            var em = context.Employees.Where(s => s.NIK == nik).FirstOrDefault();
            
            if (em != null)
            {
                var role = new AccountRole()
                {
                    NIK = em.NIK,
                    RoleID = "2"
                };
                context.AccountRoles.Add(role);
                context.SaveChanges();
                return 1;
            }
            return 0;
        }
    }
}
