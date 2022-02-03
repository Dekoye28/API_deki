using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    interface IProfilingRepository
    {
        IEnumerable<Profiling> Get();

        Profiling Get(string NIK);
        int Insert(Profiling account);
        int Update(Profiling account);
        int Delete(Profiling account);
    }
}
