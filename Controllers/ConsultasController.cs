using Consultorio.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Consultorio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        private readonly Data.AppDbContext _context;
        public ConsultasController(Data.AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Consulta>>> GetConsulta()
        {
            var consultas = await Task.Run(() => _context.Consultas.ToList());
            return Ok(consultas);
        }

        [HttpPost]
        public async Task<IActionResult> PostConsulta(Models.Consulta consulta)
        {
            var consultaEx = await _context.Consultas.FindAsync(consulta.MedicoId);
            if (consultaEx == null) return BadRequest("Médico não encontrado");

            _context.Consultas.Add(consulta);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetConsulta), new { id = consulta.Id }, consulta);
        }

        [HttpPut]
        public IActionResult PutConsulta(Models.Consulta consulta)
        {
            var consultaProgramada = _context.Consultas.Find(consulta.Id);
            if (consultaProgramada == null) return BadRequest("Consulta não encontrada");

            consultaProgramada.HorarioConsulta = consulta.HorarioConsulta;
            consultaProgramada.PacienteId = consulta.PacienteId;
            consultaProgramada.MedicoId = consulta.MedicoId;
            consultaProgramada.Observacoes = consulta.Observacoes;

            _context.SaveChanges();
            return Ok(consultaProgramada);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConsulta(int id)
        {
            var consulta = _context.Consultas.Find(id);
            if (consulta == null) return BadRequest("Id não encontrado");

            _context.Consultas.Remove(consulta);
            _context.SaveChanges();
            return Ok(consulta);
        }
    }
}
