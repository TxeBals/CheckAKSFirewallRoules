using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckAKSFirewallRoules.Models
{
    public class EndPointsConfigModel
    {

        public List<HostListModel> HostList { get; set; }       

    }


    public class HostList
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Port { get; set; }
        public string Description { get; set; }
        public int Format { get; set; }
    }




}
