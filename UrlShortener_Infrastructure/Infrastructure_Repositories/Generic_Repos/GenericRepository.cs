using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UrlShortener_Application.Application_DTOs.DTOs_Othres;
using UrlShortener_Application.Application_DTOs.DTOs_Shortener;
using UrlShortener_Application.Application_Services.Generic_Service;
using UrlShortener_Infrastructure.Infrastructure_Contexts;

namespace UrlShortener_Infrastructure.Infrastructure_Repositories.Generic_Repos
{
    public class GenericRepository<TModel> : IGenericService<TModel> where TModel : class
    {
       
        private readonly ShortenerDbContext _db;
        private readonly DbSet<TModel> _dbSet;
        private readonly ILogger<GenericRepository<TModel>> _logger;

        public GenericRepository(ShortenerDbContext context, ILogger<GenericRepository<TModel>> logger)
        {
            _db = context;
            _dbSet = _db.Set<TModel>();
            _logger = logger;
        }

        public async Task<ResponseDto<TModel>> AddAsync(TModel model)
        {
            if (model == null)
                return ResponseHelper.SetBadRequest<TModel>(message: "Model cannot be null.");

            try
            {
                await _dbSet.AddAsync(model);
                await _db.SaveChangesAsync();
                _logger.LogInformation("Added {Entity} successfully", typeof(TModel).Name);

                return ResponseHelper.SetSuccess(model, message: "Item added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddAsync failed for {Entity}", typeof(TModel).Name);
                return ResponseHelper.SetInternalServerError<TModel>(message: "An error occurred while adding the item.");
            }
        }

        public async Task<ResponseDto<TModel>> UpdateAsync(TModel model)
        {
            if (model == null)
                return ResponseHelper.SetBadRequest<TModel>(message: "Model cannot be null.");

            try
            {
                _dbSet.Update(model);
                await _db.SaveChangesAsync();
                _logger.LogInformation("Updated {Entity} successfully", typeof(TModel).Name);

                return ResponseHelper.SetSuccess(model, message: "Item updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateAsync failed for {Entity}", typeof(TModel).Name);
                return ResponseHelper.SetInternalServerError<TModel>(message: "An error occurred while updating the item.");
            }
        }

        public async Task<ResponseDto<object>> DeleteAsync(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null)
                    return ResponseHelper.SetNotFound<object>(message: $"Item with ID {id} not found.");

                _dbSet.Remove(entity);
                await _db.SaveChangesAsync();
                _logger.LogInformation("Deleted {Entity} with ID {Id}", typeof(TModel).Name, id);

                return ResponseHelper.SetSuccess<object>(data: null, message: "Item deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteAsync failed for {Entity} ID {Id}", typeof(TModel).Name, id);
                return ResponseHelper.SetInternalServerError<object>(message: "An error occurred while deleting the item.");
            }
        }

        public async Task<ResponseDto<List<TModel>>> GetAllAsync()
        {
            try
            {
                var data = await _dbSet.ToListAsync();
                return ResponseHelper.SetSuccess(data, message: "Fetched all records successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllAsync failed for {Entity}", typeof(TModel).Name);
                return ResponseHelper.SetInternalServerError<List<TModel>>(message: "Failed to fetch records.");
            }
        }

        public async Task<ResponseDto<TModel>> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null)
                    return ResponseHelper.SetNotFound<TModel>(message: $"Item with ID {id} not found.");

                return ResponseHelper.SetSuccess(entity, message: "Item retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetByIdAsync failed for {Entity}", typeof(TModel).Name);
                return ResponseHelper.SetInternalServerError<TModel>(message: "Failed to fetch record.");
            }
        }

        public async Task<ResponseDto<List<TModel>>> FindAsync(Expression<Func<TModel, bool>> predicate)
        {
            try
            {
                var results = await _dbSet.Where(predicate).ToListAsync();
                return ResponseHelper.SetSuccess(results, message: "Filtered results retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FindAsync failed for {Entity}", typeof(TModel).Name);
                return ResponseHelper.SetInternalServerError<List<TModel>>(message: "An error occurred while filtering records.");
            }
        }
    }
}
