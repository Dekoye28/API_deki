using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepositoty<MyContext, Employee, string>
    {
        private readonly MyContext context;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }

        public string idBaru() {
            var idbaru = "";
            var year = DateTime.Now.ToString("yyyy");
            var i = context.Employees.ToList().Count();
            if (i != 0)
            {
                foreach (Employee e in Get())
                {
                    idbaru = e.NIK;
                }
                idbaru = Convert.ToString(int.Parse(idbaru) + 1);
            }
            else
            {
                idbaru = year + "001";
            }
            return idbaru;
        }
        public string EdcIdBaru() {
            var id = "";
            var i = context.Educations.ToList().Count();
            if (i != 0)
            {
                foreach (Education e in context.Educations.ToList())
                {
                    id = e.EducationID;
                }
                id = Convert.ToString(int.Parse(id) + 1);
            }
            else
            {
                id = "1";
            }
            return id;

        }
        public int Register(RegisterVM req) {
            var cek = context.Employees.Where(s => s.Email == req.Email).FirstOrDefault<Employee>();
            var cek2 = context.Employees.Where(s => s.Phone == req.Phone).FirstOrDefault<Employee>();
            if (cek == null && cek2 == null)
            {
                Employee emp = new Employee
                {
                    NIK = idBaru(),
                    FirstName = req.FirstName,
                    LastName = req.LastName,
                    Birthdate = req.Birthdate,
                    Email = req.Email,
                    Phone = req.Phone,
                    Salary = req.Salary
                };
                //emp.Gender = req.Gender;
                context.Employees.Add(emp);
                //context.SaveChanges();

                Account acc = new Account
                {
                    NIK = emp.NIK,
                    password = BCrypt.Net.BCrypt.HashPassword(req.password)
                };
                context.Accounts.Add(acc);
                //context.SaveChanges();

                Education ed = new Education
                {
                    EducationID = EdcIdBaru(),
                    Degre = req.Degre,
                    GPA = req.GPA,
                    University_id = req.University_id
                };
                context.Educations.Add(ed);
                context.SaveChanges();
                Profiling prof = new Profiling
                {
                    NIK = emp.NIK,
                    EducationID = ed.EducationID
                };
                context.Profilings.Add(prof);
                AccountRole ar = new AccountRole
                {
                    NIK = emp.NIK,
                    RoleID = "1"
                };
                context.AccountRoles.Add(ar);
            }
            var hasil = context.SaveChanges();
            return hasil;
        }
      
        public IEnumerable GetRegisteredData()
        {
            var emp = context.Employees;
            var acc = context.Accounts;
            var aro = context.AccountRoles;
            var ro = context.Roles;
            var prof = context.Profilings;
            var edc = context.Educations;
            var unv = context.University;
            var req = (from Emp in emp
                       join Acc in acc on Emp.NIK equals Acc.NIK
                       join Prof in prof on Acc.NIK equals Prof.NIK
                       join Ar in aro on Acc.NIK equals Ar.NIK
                       join Ro in ro on Ar.RoleID equals Ro.RoleId
                       join Edc in edc on Prof.EducationID equals Edc.EducationID
                       join Unv in unv on Edc.University_id equals Unv.University_id
                       select new
                       {
                           FullName = string.Concat(Emp.FirstName, " ", Emp.LastName),
                           Phone = Emp.Phone,
                           BirthDate = Emp.Birthdate,
                           Salary = Emp.Salary,
                           Role = Ro.RoleName,
                           Email = Emp.Email,
                           Degree = Edc.Degre,
                           GPA = Edc.GPA,
                           UniversityName = Unv.Name
                       }
                       ).ToList();
            //var req = context.Employees.Include(a => a.Account).ToList();

            return req;
        }
   
    }
}
