using Clinicaapp.Domain.Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicaapp.Domain.Result
{
    public class OperationResult
    {
        public readonly MedicalRecord Entity;

        public OperationResult()
        {
            this.Success = true;
        }
        public string? Message { get; set; }
        public dynamic? Data { get; set; }
        public bool Success { get; set; }
    }
}
