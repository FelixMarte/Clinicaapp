using Clinicaapp.Application.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicaapp.Application.Responses.Configuration.Patients
{
    public class PatientsResponse : BaseResponse
    {
        public dynamic? Data { get; set; }
    }
}
