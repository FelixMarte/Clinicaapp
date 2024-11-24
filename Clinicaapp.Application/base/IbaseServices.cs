namespace Clinicaapp.Application.Base
{
    public interface IBaseServices<TResponse, TSaveDto, TUpdateDto>
    {
        Task<TResponse> SaveAsync(TSaveDto dto);
        Task<TResponse> UpdateAsync(TUpdateDto dto);
        Task<TResponse> GetAll();
        Task<TResponse> GetById(int Id);
        Task<TResponse> DeleteAsync(int id);

    }
}
