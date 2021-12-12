using System.Collections.Generic;

namespace WebClient.Models
{
    public class ErrorModel
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public IDictionary<string, string[]> ValidationErrors { get; set; }
    }
}
