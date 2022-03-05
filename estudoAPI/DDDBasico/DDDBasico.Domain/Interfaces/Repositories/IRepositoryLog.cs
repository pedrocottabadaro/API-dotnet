using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDBasico.Domain.Entities;

namespace DDDBasico.Domain.Interfaces
{
    public interface IRepositoryLog : IRepositoryBase<Log>
    {
        List<Log> GetUserLog(int IdUser);
        Object GetRanking();
    }
}