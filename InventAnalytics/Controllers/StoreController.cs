using InventAnalytics.Operations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace InventAnalytics.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly StoreOperations _storeOperations;
        public StoreController(StoreOperations storeOperations)
        {
            _storeOperations = storeOperations;
        }

        [HttpGet]
        [Route("{id}/Profit")]
        public async Task<IActionResult> Profit(int id)
        {
            try
            {
                return Ok(await _storeOperations.Profit(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("MostProfitableStore")]
        public async Task<IActionResult> MostProfitableStore()
        {
            try
            {
                return Ok(await _storeOperations.MostProfitableStore());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
