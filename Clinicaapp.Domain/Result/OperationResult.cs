using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicaapp.Domain.Result
{
    public class OperationResult
    {
        public string Message { get; set; }
        public bool Succes { get; set; }
        public dynamic Data { get; set; }
    }
}
