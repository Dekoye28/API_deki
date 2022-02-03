using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class ProfilingRepository : IProfilingRepository
    {
        private readonly MyContext context;
        public ProfilingRepository(MyContext context)
        {
            this.context = context;
        }
        public int Profilings { get; private set; }

        public int Delete(Profiling prof)
        {
            var emp = Get(prof.NIK);
            if (emp != null)
            {
                context.Remove(emp);
            }

            var result = context.SaveChanges();
            return result;
        }
        public IEnumerable<Profiling> Get()
        {
            return context.Profilings.ToList();

        }
        public Profiling Get(string NIK)
        {
            var get = context.Profilings.Find(NIK);
            return get;
        }
        public int Insert(Profiling prof)
        {
            var pro = context.Accounts.Find(prof.NIK);
            var edc = context.Educations.Find(prof.EducationID);
            if (context.Profilings.Find(prof.NIK) == null)
            {
                if (pro != null && edc != null)
                {
                    context.Profilings.Add(prof);
                }
            }
          
            var result = context.SaveChanges();
            return result;
        }

        public int Update(Profiling prof)
        {

            var pro = Get(prof.NIK);
            if (pro != null)
            {
                pro.EducationID = prof.EducationID;
                context.Entry(prof).State = EntityState.Modified;
            }
            var result = context.SaveChanges();
            return result;
        }

    }
}
