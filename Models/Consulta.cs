using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultorio.Api.Models
{
    public class Consulta
    {
        public int Id { get; set; }
        [ForeignKey("PacienteId")]
        public int PacienteId { get; set; }

        [ForeignKey("MedicoId")]
        public int MedicoId { get; set; }
        public DateTime HorarioConsulta { get; set; }
        public string Observacoes { get; set; }

    }
}
