using FinTech_App.Model;
using FinTech_App.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinTech_App.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public abstract class GenericController<T, K> : ControllerBase 
    { 
     private readonly IGenericService<T, long> _generalService;
    public GenericController(IGenericService<T, long> service)
    {
        _generalService = service;
    }

    // GET: api/Clients
    [HttpGet]
    public async Task<ActionResult<IEnumerable<T>>> Get()
    {
        return await _generalService.Read();
    }

    // GET: api/Clients/5
    [HttpGet("{id}")]
    public async Task<ActionResult<T>> Get(long id)
    {
        return await _generalService.Read(id);
    }

    // PUT: api/Clients/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(long id, T T)
    {
        return await _generalService.Update(id, T);
    }

    // POST: api/Clients
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<T>> PostClient(T t)
    {
        return await _generalService.Create(t);
    }

    // DELETE: api/Clients/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
         return await _generalService.Delete(id);
    }
}

