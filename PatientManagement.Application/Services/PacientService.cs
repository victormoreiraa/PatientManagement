using PatientManagement.Application.Interfaces;
using PatientManagement.Domain.Entities;
using PatientManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _pacienteRepository;

        public PatientService(IPatientRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<IEnumerable<Patient>> GetAllPacientesAsync()
        {
            return await _pacienteRepository.GetAllAsync();
        }

        public async Task<Patient> GetPacienteByIdAsync(int id)
        {
            return await _pacienteRepository.GetByIdAsync(id);
        }

        public async Task AddPacienteAsync(Patient paciente)
        {
            await _pacienteRepository.AddAsync(paciente);
        }

        public async Task UpdatePacienteAsync(Patient paciente)
        {
            await _pacienteRepository.UpdateAsync(paciente);
        }

        public async Task DeletePacienteAsync(int id)
        {
            await _pacienteRepository.DeleteAsync(id);
        }
    }
}
