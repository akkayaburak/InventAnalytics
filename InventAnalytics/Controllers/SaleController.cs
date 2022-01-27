using InventAnalytics.DTOs;
using InventAnalytics.Operations;
using InventAnalytics.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace InventAnalytics.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly SaleOperations _saleOperations;
        public SaleController(SaleOperations saleOperations)
        {
            _saleOperations = saleOperations;
        }
        [HttpGet]
        public async Task<IActionResult> SalesHistory()
        {
            try
            {
                return Ok(await _saleOperations.GetSaleHistoryAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] NewSaleDto newSaleDto)
        {
            try
            {
                await _saleOperations.Add(newSaleDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _saleOperations.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
