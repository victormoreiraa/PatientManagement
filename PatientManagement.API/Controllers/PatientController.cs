using Microsoft.AspNetCore.Mvc;
using PatientManagement.Application.Interfaces;
using PatientManagement.Domain.Entities;

namespace PatientManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _pacienteService;

        public PatientController(IPatientService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPacientes()
        {
            var pacientes = await _pacienteService.GetAllPacientesAsync();
            return Ok(pacientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPaciente(int id)
        {
            var paciente = await _pacienteService.GetPacienteByIdAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return Ok(paciente);
        }

        [HttpPost]
        public async Task<ActionResult<Patient>> PostPaciente(Patient paciente)
        {
            await _pacienteService.AddPacienteAsync(paciente);
            return CreatedAtAction(nameof(GetPaciente), new { id = paciente.Id }, paciente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaciente(int id, Patient paciente)
        {
            if (id != paciente.Id)
            {
                return BadRequest();
            }
            await _pacienteService.UpdatePacienteAsync(paciente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaciente(int id)
        {
            await _pacienteService.DeletePacienteAsync(id);
            return NoContent();
        }
    }
}
