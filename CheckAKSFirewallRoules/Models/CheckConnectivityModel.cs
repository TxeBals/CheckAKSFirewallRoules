using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckAKSFirewallRoules.Models
{
    public class HostListModel : HostList
    {
        public bool status { get; set; }
    }


    public class CheckConnectivityModel
    {
        public string AksApi { get; set; }     
        public string Region { get; set; }
        public List<HostListModel> States { get; set; }
    }
}
