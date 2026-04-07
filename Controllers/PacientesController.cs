using Consultorio.Api.Data;
using Consultorio.Api.Models;
using Consultorio.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Consultorio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PacienteService _pacienteService;

        public PacientesController(AppDbContext context)
        {
            _context = context;
            _pacienteService = new PacienteService();
        }

        [HttpPost]
        public IActionResult CreatePaciente(Models.Paciente paciente)
        {
            var erros = _pacienteService.ValidarPaciente(paciente);
            if (erros.Count > 0)
                return BadRequest(erros);
            _context.Pacientes.Add(paciente);
            _context.SaveChanges();
            return CreatedAtAction(nameof(CreatePaciente), new { id = paciente.Id }, paciente);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPaciente()
        {
            var pacientes = await Task.Run(() => _context.Pacientes.ToList());
            return Ok(pacientes);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPacienteId(int id)
        {
            var paciente = _context.Pacientes.Find(id);
            if (paciente == null) return NotFound();

            return Ok(paciente);
        }

        [HttpPut("{id}")]
        public IActionResult EditPaciente(Models.Paciente paciente)
        {
            var erros = _pacienteService.ValidarPaciente(paciente);
            if (erros.Count > 0)
                return BadRequest(erros);

            var pacienteExistente = _context.Pacientes.Find(paciente.Id);
            if (pacienteExistente == null)
                return NotFound();

            pacienteExistente.Nome = paciente.Nome;

            _context.Pacientes.Update(pacienteExistente);
            _context.SaveChanges();

            return Ok(pacienteExistente);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePaciente(int id)
        {
            var paciente = _context.Pacientes.Find(id);
            if (paciente == null)
                return NotFound();

            _context.Pacientes.Remove(paciente);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

