using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    interface IAccountRepository
    {
        IEnumerable<Account> Get();

        Account Get(string NIK);
        int Insert(Account account);
        int Update(Account account);
        int Delete(Account account);
    }
}
