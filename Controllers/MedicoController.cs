using Consultorio.Api.DTOs;
using Consultorio.Api.Models;
using Consultorio.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Consultorio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly Data.AppDbContext _context;
        public MedicoController(Data.AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Medico>>> GetMedicos()
        {
            var medicos = await Task.Run(() => _context.Medicos.ToList());
            return Ok(medicos);
        }



        [HttpPost]
        public async Task<IActionResult> PostMedico(Models.Medico medico)
        {
            var consultorioEx = await _context.Consultorios.FindAsync(medico.ConsultorioId);
            if (consultorioEx == null) return BadRequest("Consultório não encontrado");

            _context.Medicos.Add(medico);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetMedicos), new { id = medico.Id }, medico);
        }

        [HttpPut]
        public IActionResult PutMedico(Models.Medico medico)
        {
            var medicoExistente = _context.Medicos.Find(medico.Id);
            if (medicoExistente == null)
                return NotFound("Id não Encontrado");

            medicoExistente.Nome = medico.Nome;
            medicoExistente.Crm = medico.Crm;
            medicoExistente.ConsultorioId = medico.ConsultorioId;

            _context.Medicos.Update(medicoExistente);
            _context.SaveChanges();

            return Ok(medicoExistente);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMedico(int id)
        {
            var medico = _context.Medicos.Find(id);
            if (medico == null)
                return NotFound("Id não Encontrado");

            _context.Medicos.Remove(medico);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
