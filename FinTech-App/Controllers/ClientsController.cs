using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinTech_App.Model;
using FinTech_App.Service;

namespace FinTech_App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : GenericController<Client, long>
{
    public ClientsController(IGenericService<Client, long> service) : base(service)
    {
    }
}