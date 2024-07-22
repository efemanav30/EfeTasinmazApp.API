using Microsoft.AspNetCore.Mvc;
using Tasinmaz_Proje.Entities;
using Tasinmaz_Proje.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tasinmaz_Proje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        // GET: api/Log
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Log>>> GetLogs()
        {
            var logs = await _logService.GetAllAsync();
            return Ok(logs);
        }

        // GET: api/Log/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Log>> GetLog(int id)
        {
            var log = await _logService.GetByIdAsync(id);
            if (log == null)
            {
                return NotFound();
            }
            return Ok(log);
        }

        // POST: api/Log
        [HttpPost]
        public async Task<ActionResult<Log>> AddLog(Log log)
        {
            var createdLog = await _logService.AddAsync(log);
            return CreatedAtAction(nameof(GetLog), new { id = createdLog.Id }, createdLog);
        }

        // PUT: api/Log/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Log>> UpdateLog(int id, Log log)
        {
            if (log == null)
            {
                return BadRequest();
            }

            var updatedLog = await _logService.UpdateAsync(id, log);
            if (updatedLog == null)
            {
                return NotFound();
            }
            return Ok(updatedLog);
        }

        // DELETE: api/Log/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLog(int id)
        {
            var result = await _logService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
