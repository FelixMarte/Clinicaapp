using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicaapp.Application.Core
{
    public abstract class BaseResponse
    {
        public bool Succes { get; set; }
        public string? Message { get; set; }
    }
}
