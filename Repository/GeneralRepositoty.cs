using API.Context;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class GeneralRepositoty<Context, Entity, Key> : IRepository<Entity, Key>
        where Entity : class
        where Context : MyContext
    {
        private readonly MyContext myContext; 
        private readonly DbSet<Entity> entities;

        public GeneralRepositoty(MyContext myContext)
        {
            this.myContext = myContext;
            entities = myContext.Set<Entity>();
        }

        public int Delete(Key key)
        {
            var del = entities.Find(key);
            if (del != null)
            {
                entities.Remove(del);
            }
            //entities.Remove(del);
            var hasil = myContext.SaveChanges();
            return hasil;
        }

        public IEnumerable<Entity> Get()
        {
            
            return entities.ToList();
        }

        public Entity Get(Key key)
        {
            var b = entities.Find(key);
            return b;
        }

        public int Insert(Entity entity)
        {
            entities.Add(entity);
            var hasil = myContext.SaveChanges();
            return hasil;
        }

        public int Update(Entity entity)
        {
            myContext.Entry(entity).State = EntityState.Modified;
            var hasil = myContext.SaveChanges();
            return hasil;
        }
    }
}
