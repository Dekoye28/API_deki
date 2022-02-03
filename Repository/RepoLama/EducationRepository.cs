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
    public class EducationRepository : IEducationRepository
    {
        private readonly MyContext context;
        public EducationRepository(MyContext context)
        {
            this.context = context;
        }
        public int Educations { get; private set; }

        public int Delete(Education ed)
        {
            var edc = Get(ed.EducationID);
            if (edc != null)
            {
                context.Remove(edc);
            }

            var result = context.SaveChanges();
            return result;
        }
        public IEnumerable<Education> Get()
        {
            return context.Educations.ToList();

        }
        public Education Get(string id)
        {
            var get = context.Educations.Find(id);
            return get;
        }
        public int Insert(Education ed)
        {
            context.Educations.Add(ed);
            var result = context.SaveChanges();
            return result;
        }

        public int Update(Education ed)
        {

            var edc = Get(ed.EducationID);
            if (edc != null)
            {
                context.Entry(ed).State = EntityState.Modified;
            }
            var result = context.SaveChanges();
            return result;
        }

    }
}
