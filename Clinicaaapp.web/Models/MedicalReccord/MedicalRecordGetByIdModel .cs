using Clinicaaapp.web.Models;
using Clinicaapp.Domain.Entities.Configuration;

namespace Clinicaapp.Web.Models.MedicalRecords
{
    public class MedicalRecordGetByIdModel : BaseApiResponseModel
    {
        public MedicalRecordModel? Data { get; set; }
    }
}