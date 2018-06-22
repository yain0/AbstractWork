using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AbstractWorkService.ViewModels
{
    [DataContract]
   public class SkladsLoadViewModel
    {
        [DataMember]
        public string SkladName { get; set; }
        [DataMember]
        public int TotalKoll { get; set; }
        [DataMember]
        public List<SkladsMaterialLoadViewModel> Materials { get; set; }
    }
    [DataContract]
    public class SkladsMaterialLoadViewModel
    {
        [DataMember]
        public string MaterialName { get; set; }

        [DataMember]
        public int Koll { get; set; }
    }
}
