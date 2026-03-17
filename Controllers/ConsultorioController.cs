using Consultorio.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Consultorio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultorioController : ControllerBase
    {
        private readonly Data.AppDbContext _context;
        private readonly Services.ViaCepService _viaCepService;

        public ConsultorioController(Data.AppDbContext context, Services.ViaCepService viaCepService)
        {
            _context = context;
            _viaCepService = viaCepService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Consultorio>>> GetConsultorios()
        {
            var consultorios = await Task.Run(() => _context.Consultorios.ToList());
            return Ok(consultorios);
        }


        [HttpPost]
        public async Task<IActionResult> PostConsultorio(Models.Consultorio consultorio)
        {
            var endereco = await _viaCepService.BuscarEnderecoAsync(consultorio.Cep);

            if (endereco != null)
            {
                consultorio.Logradouro = endereco.logradouro;
                consultorio.Bairro = endereco.bairro;
                consultorio.Localidade = endereco.localidade;
                consultorio.Uf = endereco.uf;
            }
            else
            {
                return BadRequest("CEP inválido ou não encontrado.");
            }
            _context.Consultorios.Add(consultorio);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetConsultorios), new { id = consultorio.Id }, consultorio);
        }

        [HttpPut]
        public IActionResult EditConsultorio(Models.Consultorio consultorio)
        {

            var consultorioExistente = _context.Consultorios.Find(consultorio.Id);
            if (consultorioExistente == null)
                return NotFound();

            consultorioExistente.Nome = consultorio.Nome;

            if (consultorioExistente == null)
            {
                return BadRequest("CEP inválido ou não encontrado.");
            }
            else
            {

                consultorio.Logradouro = consultorioExistente.Logradouro;
                consultorio.Bairro = consultorioExistente.Bairro;
                consultorio.Localidade = consultorioExistente.Localidade;
                consultorio.Uf = consultorioExistente.Uf;

            }

            _context.Consultorios.Update(consultorioExistente);
            _context.SaveChanges();

            return Ok(consultorioExistente);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConsultorio(int id)
        {
            var consultorio = _context.Consultorios.Find(id);
            if (consultorio == null)
                return NotFound();

            _context.Consultorios.Remove(consultorio);
            _context.SaveChanges();

            return NoContent();
        }


    }
}