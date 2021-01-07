using JWTDemoClient.Attributes;
using JWTDemoClient.Models;
using JWTDemoClient.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace JWTDemoClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OderService oderService;

        public OrderController(
            OderService oderService
        )
        {
            this.oderService = oderService;
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<Order> Get() => oderService.ListAll();

        [AuthorizeOnJwtSource]
        [HttpGet("mine")]
        public IEnumerable<Order> GetMine() => oderService.ListByCustomer(User.Identity.Name);
    }
}
