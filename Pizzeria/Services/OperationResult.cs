namespace Pizzeria.Services
{
    public class OperationResult
    {
        public bool Success { get; set; } = true;
        public List<FieldError> Errors { get; set; } = new List<FieldError>();
    }


    public class FieldError
    {
        public string FieldKey { get; set; }
        public string ErrorMsg { get; set; }
    }
}
