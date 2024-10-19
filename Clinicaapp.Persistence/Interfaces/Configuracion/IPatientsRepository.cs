using Clinicaapp.Domain.Result;


namespace Clinicaapp.Persistence.Interfaces.Configuracion
{
    public interface IPatientsRepository
    {
        List<OperationResult> SavePatientByPatientID(int PatientID);
        List<OperationResult> GetPatientByPatientID(int PatientID);
        List<OperationResult> UpdatePatientByPatientID(int PatientID);
        List<OperationResult> DeletePatientByPatientID(int PatientID);
    }
}
