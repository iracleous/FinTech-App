using FinTech_App.Model;
using FinTech_App.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinTech_App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : GenericController<Account, long>
{
    public AccountController(IGenericService<Account, long> service) : base(service)
    {
    }
}
