using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CheckAKSFirewallRoules.Models
{
    public class CustomResponse
    {
        public string fullResponse { get; set; }
        public HttpStatusCode statusCode { get; set; }
        public string otherCodes { get; set; }

    }
}
