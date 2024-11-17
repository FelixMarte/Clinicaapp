using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicaapp.Application.Base
{
    public interface IBaseServices<TResponse, TSaveDto, TUpdateDto>
    {
        Task<TResponse> SaveAsync(TSaveDto dto);
        Task<TResponse> UpdateAsync(TUpdateDto dto);
        Task<TResponse> GetAll();
        Task<TResponse> GetById(int Id);
    }
}
