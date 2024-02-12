using FinTech_App.Dto;
using FinTech_App.Model;
using FinTech_App.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FinTech_App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : GenericController<Account, long>
{
    public AccountController(IGenericService<Account, long> service) : base(service)
    {
    }


    [HttpGet]
    [Route("Data")]
    public List<GraphDataDto> GetGraphData()
    {
        return   new()
            {
                new (){   Country="Italy", Size=55 },
                 new (){   Country="France", Size=49 },
                 new (){   Country="Spain", Size=44 },
            };

    }
}
