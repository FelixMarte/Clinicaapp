using Clinicaapp.Domain.Repositories;
using Clinicaapp.Domain.Result;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using BoletosApp.Persistance.Context;
using Clinicaapp.Domain.Entities.Configuration;

namespace Clinicaapp.Persistance.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ClinicaContext _Clinicacontext;
        private DbSet<TEntity> entities;

        public BaseRepository(ClinicaContext Clinicacontext)
        {
            _Clinicacontext = Clinicacontext;
            this.entities = _Clinicacontext.Set<TEntity>();
        }
        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> filter)
        {
            return await this.entities.AnyAsync(filter);
        }

        public virtual async Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();

            try
            {
                var datos = await this.entities.ToListAsync();
                result.Data = datos;
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = $"Ocurrió un error {ex.Message} obteniendo los datos.";
            }

            return result;
        }

        public async Task<OperationResult> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            OperationResult result = new OperationResult();

            try
            {
                var datos = await this.entities.Where(filter).ToListAsync();
                result.Data = datos;
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = $"Ocurrió un error {ex.Message} obteniendo los datos.";
            }

            return result;

        }

        public virtual async Task<OperationResult> GetEntityBy(int Id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var entity = await this.entities.FindAsync(Id);
                result.Data = entity;
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = $"Ocurrió un error {ex.Message} obteniendo la entidad.";
            }
            return result;
        }

        public async virtual Task<OperationResult> Remove(int Id)
        {
            OperationResult result = new OperationResult();

            try
            {

                var entity = await entities.FindAsync(Id);

                if (entity != null)
                {
                    entities.Remove(entity);
                    await _Clinicacontext.SaveChangesAsync();
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.Message = "Entidad no encontrada.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió un error {ex.Message} .detalle{ex.InnerException?.Message} ";
            }

            return result;
        }


        public async virtual Task<OperationResult> Save(TEntity entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                entities.Add(entity);
                await _Clinicacontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió un error {ex.Message} guardando la entidad.";

            }

            return result;
        }

        public async virtual Task<OperationResult> Update(TEntity entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                entities.Update(entity);
                await _Clinicacontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió un error {ex.Message} actualizando la entidad.";

            }

            return result;
        }
    }
}

