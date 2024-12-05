using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SparePartsInventory._00015850.Data;
using SparePartsInventory._00015850.Models;
using SparePartsInventory._00015850.Models.Entities;

namespace SparePartsInventory._00015850.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class SparePartsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public SparePartsController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var spareParts = await _dbContext.SpareParts
                .Include(s => s.Producer)
                .ToListAsync();
            
            return Ok(spareParts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var sparePart = await _dbContext.SpareParts
                .Include(sp => sp.Producer)
                .FirstOrDefaultAsync(sp => sp.Id == id);
            if (sparePart is null)
            {
                return NotFound();
            }
            
            return Ok(sparePart);
        }
 

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddSparePartDto addSparePartDto)
        {
            // check producer for exist
            var producer = await _dbContext.Producers.FindAsync(addSparePartDto.ProducerId);
            if (producer is null)
            {
                return BadRequest("Producer not found");
            }
            
            var sparePartEntity = new SparePart()
            {
                Name = addSparePartDto.Name,
                Description = addSparePartDto.Description,
                Price = addSparePartDto.Price,
                ProducerId = addSparePartDto.ProducerId
            };
            
            var sparePartEntityEntry = await _dbContext.SpareParts.AddAsync(sparePartEntity);
            await _dbContext.SaveChangesAsync();
            
            return Ok(sparePartEntityEntry.Entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateSparePart updateSparePart)
        {
            var sparePart = await _dbContext.SpareParts.FindAsync(id);
            if (sparePart is null)
            {
                return NotFound();
            }
            
            // check producer for exist
            var producer = await _dbContext.Producers.FindAsync(updateSparePart.ProducerId);
            if (producer is null)
            {
                return BadRequest("Producer not found");
            }
            
            sparePart.Name = updateSparePart.Name;
            sparePart.Description = updateSparePart.Description;
            sparePart.Price = updateSparePart.Price;
            sparePart.ProducerId = updateSparePart.ProducerId;
            
            await _dbContext.SaveChangesAsync();
            return Ok(sparePart);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var sparePart = await _dbContext.SpareParts.FindAsync(id);

            if (sparePart is null)
            {
                return NotFound();
            }

            _dbContext.SpareParts.Remove(sparePart);

            return Ok(await _dbContext.SaveChangesAsync() > 0);
        }
    }
}
