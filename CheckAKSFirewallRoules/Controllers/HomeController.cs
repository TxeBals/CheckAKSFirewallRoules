using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CheckAKSFirewallRoules.Models;
using Microsoft.Extensions.Configuration;
using CheckAKSFirewallRoules.Code;

namespace CheckAKSFirewallRoules.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;



        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

       

        public IActionResult CheckConnectivity(string api, string region)
        {

            var endPoints = new EndPointsConfigModel();            
            endPoints.HostList = _configuration.GetSection("HostList").Get<List<HostListModel>>();

            endPoints.HostList.Add(new HostListModel() { Description = "API Aks", Format = 0, Name = "Api AKS", Port = "443", Url = api });

            if (endPoints.HostList == null)
                throw new Exception("Error on load configuration settings");

            var model = new CheckConnectivityModel() { AksApi = api, Region = region, States = endPoints.HostList};

            return View(model);
        }

        public IActionResult TestConnectivity(string api, string region, string name)
        {
            var endPoints = new EndPointsConfigModel();
            endPoints.HostList = _configuration.GetSection("HostList").Get<List<HostListModel>>();

            endPoints.HostList.Add(new HostListModel() { Description = "API Aks", Format = 0, Name = "Api AKS", Port = "443", Url = api });

            if (endPoints.HostList.Count(x=>x.Name == name) == 0)
            {
                return new BadRequestObjectResult(new { Alert = "true", Message = name + " not found in configuration list" });
            }
           
            return new OkObjectResult(CheckUrlUtil.Test(endPoints.HostList.Where(x=>x.Name == name).FirstOrDefault(),api,region));
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
