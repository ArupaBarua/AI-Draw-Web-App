using AIDrawWebAPI.Data;
using AIDrawWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIDrawWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIDrawController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AIDrawController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpPost("save")]
        public async Task<ActionResult> SaveDrawing([FromBody] Drawing drawing)
        {
            if (drawing == null || string.IsNullOrWhiteSpace(drawing.Name))
            {
                return BadRequest("Drawing name not valid!");
            }
            if (drawing.CreatedAt == default(DateTime))
                drawing.CreatedAt = DateTime.UtcNow;

            var existing = await context.Drawings
                .FirstOrDefaultAsync(d => d.Name == drawing.Name);

            if(existing != null)
            {
                existing.StrokeData = drawing.StrokeData;
                context.Drawings.Update(existing);
            }
            else
            {
                await context.Drawings.AddAsync(drawing);
            }
                
            await context.SaveChangesAsync();
            return Ok("Drawing saved successfully.");
        }


        [HttpGet("load/{name}")]
        public async Task<ActionResult<Drawing>> LoadDrawing(string name)
        {
            var drawing = await context.Drawings
                .FirstOrDefaultAsync(d => d.Name == name);
            if (drawing == null)
            {
                return NotFound("Drawing not found.");
            }
            return Ok(drawing);
        }


        [HttpDelete("delete/{name}")]
        public async Task<ActionResult> DeleteDrawing(string name)
        {
            var drawing = await context.Drawings
                .FirstOrDefaultAsync(d => d.Name == name);
            if (drawing == null)
            {
                return NotFound("Drawing not found.");
            }
            context.Drawings.Remove(drawing);
            await context.SaveChangesAsync();
            return Ok("Drawing deleted successfully.");
        }
    }
}
