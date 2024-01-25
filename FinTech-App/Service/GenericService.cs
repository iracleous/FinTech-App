﻿namespace FinTech_App.Service;

using FinTech_App.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



public class GenericService<T, K> : IGenericService<T, K> where T : class, GenericModel<K>
{
    private readonly FinTechDbContext _context;
    private ILogger<ClientService> _logger;

    public GenericService(FinTechDbContext context, ILogger<ClientService> logger)
    {
        _context = context;
        _logger = logger;
    }


    public async Task<ActionResult<T>> Create(T t)
    {
        _logger.LogInformation("Method Create starting");
        _context.Set<T>().Add(t);
        await _context.SaveChangesAsync();
        return t;
    }

    public async Task<IActionResult> Delete(K id)
    {
        _logger.LogInformation("Method Delete starting");
        var t = await _context.Set<T>().FindAsync(id);
        if (t == null)
        {
            return new NotFoundResult();
        }
        _context.Set<T>().Remove(t);
        await _context.SaveChangesAsync();

        return new NoContentResult();
    }

    public async Task<ActionResult<T>> Read(K id)
    {
        _logger.LogInformation("Method Read starting");
        var t = await _context.Set<T>().FindAsync(id);
        if (t == null)
        {
            return new NotFoundResult();
        }

        return t;
    }

    public async Task<ActionResult<IEnumerable<T>>> Read()
    {
        _logger.LogInformation("Method Read starting");
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<IActionResult> Update(K id, T t)
    {
        _logger.LogInformation("Method UpdateClient starting");
        if (! (id.Equals(t.Id)))
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
                throw;
            }
        }

        return new NoContentResult();
    }


    private bool ClientExists(K id)
    {
        return _context.Set<T>().Any(e => id.Equals(e.Id));
    }
}




