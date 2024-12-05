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
    public class ProducersController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ProducersController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var producers = await _dbContext.Producers
                .Include(p => p.SpareParts)
                .ToListAsync();
            
            return Ok(producers);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var producer = await _dbContext.Producers
                .Include(p => p.SpareParts)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producer is null)
            {
                return NotFound();
            }
            return Ok(producer);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddProducerDto addProducerDto) 
        {
            var producerEntity = new Producer()
            {
                Name = addProducerDto.Name,
                Country = addProducerDto.Country,
                FoundedDate = addProducerDto.FoundedDate
            };

            var createdProducerEntry = await _dbContext.Producers.AddAsync(producerEntity);
            await _dbContext.SaveChangesAsync();
            
            return Ok(createdProducerEntry.Entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateProducerDto updateProducer)
        {
            var producer = await _dbContext.Producers.FindAsync(id);
            if (producer is null)
            {
                return NotFound();
            }
            
            producer.Name = updateProducer.Name;
            producer.Country = updateProducer.Country;
            producer.FoundedDate = updateProducer.FoundedDate;

            await _dbContext.SaveChangesAsync();
            
            return Ok(producer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var producer = await _dbContext.Producers.FindAsync(id);

            if (producer is null)
            {
                return NotFound();
            }
            _dbContext.Producers.Remove(producer);
            
            return Ok(await _dbContext.SaveChangesAsync() > 0);
        }
    }
}
