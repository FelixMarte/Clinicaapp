using Clinicaapp.Domain.Repositories;
using Clinicaapp.Domain.Result;
using Clinicaapp.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Clinicaapp.Persistence.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ClinicaContext _clinicacontext;
        private DbSet<TEntity> entities;
        public BaseRepository(ClinicaContext clinicacontext)
        {
            _clinicacontext = clinicacontext;
            this.entities = _clinicacontext.Set<TEntity>();   
        }

        public virtual async Task<OperationResult> Delete(TEntity entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                this.entities.Remove(entity);  
                await this._clinicacontext.SaveChangesAsync();  

                result.Succes = true;
                result.Message = "Entidad eliminada exitosamente";
            }
            catch (Exception ex)
            {
                result.Succes = false;
                result.Message = $"Error al eliminar la entidad: {ex.Message}";
            }

            return result;
        }

        public virtual async Task<OperationResult> Exists(Expression<Func<TEntity, bool>> filter)
        {
            bool exists = await this.entities.AnyAsync(filter);

            return new OperationResult
            {
                Succes = exists,
                Message = exists ? "Entity exists" : "Entity does not exist"
            };
        }

        public virtual async Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();
            try
            {
                var dt = await this.entities.ToListAsync();
                result.Data = dt;

            }
            catch (Exception ex)
            {
                result.Succes = false;
                result.Message = $"Error al obtener los datos: {ex.Message}";
            }
            return result;
        }

        public virtual async  Task<OperationResult> GetEntityBy(int Id)
        {
            OperationResult result = new OperationResult();
            try
            {
                
                var entity = await this.entities.FindAsync(Id);

                if (entity != null)
                {
                    result.Succes = true;
                    result.Data = entity;
                    result.Message = "Entidad encontrada exitosamente.";
                }
                else
                {
                    result.Succes = false;
                    result.Message = "Entidad no encontrada.";
                }
            }
            catch (Exception ex)
            {
                result.Succes = false;
                result.Message = $"Error al buscar la entidad: {ex.Message}";
            }

            return result;
        }

        public virtual async Task<OperationResult> Save(TEntity entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                
                if (this._clinicacontext.Entry(entity).State == EntityState.Detached)
                {
                    await this.entities.AddAsync(entity); 
                    result.Message = "Entidad guardada exitosamente.";
                }
                else
                {
                    this.entities.Update(entity); 
                    result.Message = "Entidad actualizada exitosamente.";
                }

                await this._clinicacontext.SaveChangesAsync(); 

                result.Succes = true;
                result.Data = entity;
            }
            catch (Exception ex)
            {
                result.Succes = false;
                result.Message = $"Error al guardar la entidad: {ex.Message}";
            }

            return result;
        }

        public virtual async Task<OperationResult> Update(TEntity entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                
                var trackedEntity = await this.entities.FindAsync(entity); 

                if (trackedEntity != null)
                {
                    this._clinicacontext.Entry(trackedEntity).CurrentValues.SetValues(entity); 
                    await this._clinicacontext.SaveChangesAsync(); 

                    result.Succes = true;
                    result.Message = "Entidad actualizada exitosamente.";
                    result.Data = entity;
                }
                else
                {
                    result.Succes = false;
                    result.Message = "Entidad no encontrada.";
                }
            }
            catch (Exception ex)
            {
                result.Succes = false;
                result.Message = $"Error al actualizar la entidad: {ex.Message}";
            }

            return result;
        }
    }
}
