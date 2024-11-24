namespace Clinicaapp.Application.core
{
    public abstract class BaseResponse
    {
        public bool Succes { get; set; }
        public string? Message { get; set; }
    }
}
