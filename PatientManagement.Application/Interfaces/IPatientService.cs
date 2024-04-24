using PatientManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Application.Interfaces
{ 
        public interface IPatientService
        {
            Task<IEnumerable<Patient>> GetAllPacientesAsync();
            Task<Patient> GetPacienteByIdAsync(int id);
            Task AddPacienteAsync(Patient paciente);
            Task UpdatePacienteAsync(Patient paciente);
            Task DeletePacienteAsync(int id);
        }   
}
