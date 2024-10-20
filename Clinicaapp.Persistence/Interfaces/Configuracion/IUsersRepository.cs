﻿using Clinicaapp.Domain.Entities.Configuration;
using Clinicaapp.Domain.Repositories;
using Clinicaapp.Domain.Result;


namespace Clinicaapp.Persistence.Interfaces.Configuracion
{
    public interface IUsersRepository : IBaseRepository<Users>
    {
        List<OperationResult> GetUsersByUserID(int UserID);
  
    }
}
