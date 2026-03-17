using System.ComponentModel.DataAnnotations.Schema;

namespace Consultorio.Api.Models;

public class Medico
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Crm { get; set; }

    [ForeignKey("ConsultorioId")]
    public int ConsultorioId { get; set; }

}
