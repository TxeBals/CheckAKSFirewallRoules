using CheckAKSFirewallRoules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace CheckAKSFirewallRoules.Code
{
    public static class CheckUrlUtil
    {

        public static CustomResponse Test(HostList info, string api, string region)
        {

            var result = new CustomResponse();
            var _url = string.Empty;
            switch (info.Port)
            {
                case "80":
                    _url = "http://" + info.Url;
                    break;
                default:
                    _url = "https://" + info.Url;
                    break;
            }

            switch (info.Format)
            {
                case 1:
                    _url = string.Format(_url, region);
                    break;                                    
                default:
                    break;
            }

            var client = new HttpClient();
            try
            {
                var response = client.GetAsync(_url);
                response.Wait();

                result.statusCode = response.Result.StatusCode;
                result.fullResponse = response.Result.Content.ReadAsStringAsync().Result.ToString();
            }
            catch(SocketException ex)
            {
                result.otherCodes = ex.SocketErrorCode.ToString();
                result.fullResponse = ex.Message;
            }
            catch(Exception ec)
            {
                result.otherCodes = "999";
                result.fullResponse =ec.Message;
            }

            return result;

        }


    }
}
