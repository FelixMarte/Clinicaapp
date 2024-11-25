using Clinicaaapp.web.Models;
using Clinicaapp.Domain.Entities.Configuration;

namespace Clinicaapp.Web.Models.MedicalRecords
{
    public class MedicalRecordGetAllResultModel : BaseApiResponseModel
    {
        public List<MedicalRecordModel>? data { get; set; }
    }
}