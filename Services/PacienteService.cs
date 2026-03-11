using Consultorio.Api.Models;

namespace Consultorio.Api.Services
{
    public class PacienteService
    {
        public List<string> ValidarPaciente(Paciente paciente)
        {
            List<string> erros = new List<string>();

            if (paciente.Nome == null || paciente.Nome == "")
                erros.Add("O nome é obrigatório.");

            if (paciente.Email == null || paciente.Email == "")
                erros.Add("O Email é obrigatório.");
            else if (!paciente.Email.Contains("@"))
                erros.Add("Digite um Email válido.");

            if (paciente.CPF == null || paciente.CPF == "")
                erros.Add("O CPF é obrigatório.");
            else if (paciente.CPF.Length != 11)
                erros.Add("O CPF deve ter 11 caracteres.");
            

                return erros;
        }
    }
}
