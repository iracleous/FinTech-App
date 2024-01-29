using Microsoft.AspNetCore.Mvc;

namespace FinTech_App.Service;

public interface IGenericService<T, K>
{
    //CRUD 5 services
    public Task<ActionResult<T>> Create(T t);
    public Task<ActionResult<T>> Read(K id);
    public Task<ActionResult<IEnumerable<T>>> Read();
    public Task<IActionResult> Update(K id, T t);
    public Task<IActionResult> Delete(K id);
}
