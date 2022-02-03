using API.Context;
using API.Models;
using API.ViewModel;
//using Castle.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepositoty<MyContext, Account, string>
    {
        private readonly MyContext context;
        public IConfiguration  _configuration;
        public AccountRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            this.context = myContext;
            this._configuration = configuration;

        }
        public IEnumerable ProfileVM(string requ)
        {
            var emp = context.Employees;
            var acc = context.Accounts;
            var prof = context.Profilings;
            var edc = context.Educations;
            var unv = context.University;
            var req = (from Emp in emp
                       join Acc in acc on Emp.NIK equals Acc.NIK
                       join Prof in prof on Acc.NIK equals Prof.NIK
                       join Edc in edc on Prof.EducationID equals Edc.EducationID
                       join Unv in unv on Edc.University_id equals Unv.University_id
                       where requ == Emp.Email
                       select new
                       {
                           FullName = string.Concat(Emp.FirstName, " ", Emp.LastName),
                           Phone = Emp.Phone,
                           BirthDate = Emp.Birthdate,
                           Salary = Emp.Salary,
                           Email = Emp.Email,
                           Degree = Edc.Degre,
                           GPA = Edc.GPA,
                           University_name = Unv.Name
                       }
                       ).ToList();

            return req;
        }
        public int LoginVM(RegisterVM req)
        {
            var cek = context.Employees.Where(s => s.Email == req.Email || s.Phone == req.Phone).FirstOrDefault<Employee>();
            var hasil = 0;
            if (cek != null)
            {
                var cekPass = context.Accounts.Where(s => s.NIK == cek.NIK).FirstOrDefault();

                if (BCrypt.Net.BCrypt.Verify(req.password, cekPass.password))
                {
                    hasil = 1;
                }
                else
                {
                    hasil = 2;
                }
            }

            return hasil;
        }
        public IEnumerable GetToken(string email)
        {
            var getUserData = context.Employees.Where(s => s.Email == email).FirstOrDefault();
            var acc = context.Accounts.Where(a => a.NIK == getUserData.NIK).FirstOrDefault();
            var role = context.AccountRoles.Where(r => r.NIK == acc.NIK).FirstOrDefault();
            var hit = context.AccountRoles.Where(r => r.NIK == acc.NIK).Count();
            var a = role.Role.RoleName;
            if (hit>1)
            {
                a = "Manager";
            }
            var claims = new List<Claim>
            {
                new Claim ("Email", getUserData.Email),
                new Claim ("roles", a)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    _configuration["Jwt : Issuer"],
                    _configuration["Jwt : Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn
                );
            var idToken = new JwtSecurityTokenHandler().WriteToken(token);
            claims.Add(new Claim("TokenSecurity", idToken.ToString()));
            return idToken;
        }
        public int GenerateOtp(string s, string otp)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(s);
            mail.From = new MailAddress("dekoys2828@gmail.com", "HELLOW GUYSS", System.Text.Encoding.UTF8);
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = "Kode OTP = " + otp;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("dekoys2828@gmail.com", "awanawan28");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            try
            {
                client.Send(mail);

            }
            catch (Exception ex)
            {
                Exception ex2 = ex;
                string errorMessage = string.Empty;
                while (ex2 != null)
                {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;
                }
            }
            return 0;
        }
        public int ForgotPass(string email) {
            var m = context.Employees.Where(s => s.Email == email).FirstOrDefault();
            var n = context.Accounts.Where(s => s.NIK == m.NIK).FirstOrDefault();
            Random x = new Random();
            var kirim = x.Next(11111,99999);
            if (n != null)
            {
                n.otp = kirim;
                n.ExpiredTime = DateTime.Now.AddMinutes(5);
                n.isTrue = false;
                context.Entry(n).State = EntityState.Modified;
                context.SaveChanges();
                GenerateOtp(email,kirim.ToString());
                return 1;
            }
           
            return 0;
        }
        public int ChangePass(changePass pass) 
        {
            var m = context.Employees.Where(s => s.Email == pass.email).FirstOrDefault();
            var n = context.Accounts.Where(s => s.NIK == m.NIK).FirstOrDefault();
           
            if (m != null)
            {
                if (DateTime.Now <= n.ExpiredTime)
                {
                    if (n.otp == pass.OTP)
                    {
                        if (n.isTrue == false)// DEKOYS PROGRAMMING KEREN
                        {
                            if (pass.pass == pass.conPass)
                            {
                                n.password = BCrypt.Net.BCrypt.HashPassword(pass.conPass);
                                n.isTrue = true;
                                context.Entry(n).State = EntityState.Modified;
                                context.SaveChanges();
                                return 1;
                            }
                            else
                            {
                                return 3;
                            }
                        }
                        else
                        {
                            return 5;
                        }
                    }
                    return 4;
                    
                }
                else
                {
                    return 2;
                }
               
            }
            return 0;
        }
    }
}
