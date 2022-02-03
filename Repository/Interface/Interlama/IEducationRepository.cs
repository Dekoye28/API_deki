using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    interface IEducationRepository
    {
        IEnumerable<Education> Get();

        Education Get(string NIK);
        int Insert(Education education);
        int Update(Education education);
        int Delete(Education education);
    }
}
