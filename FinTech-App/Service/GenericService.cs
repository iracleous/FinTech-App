namespace FinTech_App.Service;

using FinTech_App.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class GenericService<T, K> : IGenericService<T, K> where T : class, IGenericModel<K>
{
    protected readonly FinTechDbContext _context;
    protected readonly ILogger<GenericService<T, K>> _logger;

    public GenericService(FinTechDbContext context, ILogger<GenericService<T, K>> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="t"></param>
    /// <returns>the input object with Id from the db</returns>
    public async Task<ActionResult<T>> Create(T t)
    {
        _logger.LogInformation("Method Create starting");
        try
        {
            _context.Set<T>().Add(t);
            await _context.SaveChangesAsync();
            var result = new CreatedAtActionResult("", "GetT", new { id = t.Id }, t);
            return t;
        }
        catch(Exception )
        {
            return new BadRequestResult();
        }
     }

    public async Task<IActionResult> Delete(K id)
    {
        _logger.LogInformation("Method Delete starting");
        try 
        { 
            var t = await _context.Set<T>().FindAsync(id);
            if (t == null)
            {
                return new NotFoundResult();
            }
            _context.Set<T>().Remove(t);
            await _context.SaveChangesAsync();
            return new NoContentResult();
        }
        catch (Exception)
        {
            return new BadRequestResult();
        }
    }

    public async Task<ActionResult<T>> Read(K id)
    {
        _logger.LogInformation("Method Read starting");
        try
        {
            var t = await _context.Set<T>().FindAsync(id);
            if (t == null)
            {
                return new NotFoundResult();
            }
            return t;
        }
        catch (Exception)
        {
            return new BadRequestResult();
        }
    }

    public async Task<ActionResult<IEnumerable<T>>> Read()
    {
        _logger.LogInformation("Method Read starting");
        try
        {
            return await _context.Set<T>().ToListAsync();
        }
        catch (Exception)
        {
            return new BadRequestResult();
        }
    }

    public async Task<IActionResult> Update(K id, T t)
    {
        _logger.LogInformation("Method UpdateClient starting");
        if (id == null)
        {
            return new NotFoundResult();
        }
        if (!id.Equals(t.Id))
        {
            return new BadRequestResult();
        }
        _context.Entry(t).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ClientExists(id))
            {
                return new NotFoundResult();
            }
            else
            {
                //Ticket T421 the following line was modified
                //throw;
                return new BadRequestResult();
            }
        }
        return new NoContentResult();
    }

    private bool ClientExists(K id)
    {
        if (id == null)
        {
            return false;
        }
        return _context.Set<T>().Any(e => id.Equals(e.Id));
    }
}





