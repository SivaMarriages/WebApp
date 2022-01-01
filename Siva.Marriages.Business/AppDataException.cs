
namespace Siva.Marriages.Business
{
    public class AppDataException : Exception
    {
        public int StatusCode { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
