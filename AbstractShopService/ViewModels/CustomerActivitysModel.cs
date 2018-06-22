using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AbstractWorkService.ViewModels
{
    [DataContract]
    public class CustomerActivitysModel
    {
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public string DateCreate { get; set; }
        [DataMember]
        public string RemontName { get; set; }
        [DataMember]
        public int Koll { get; set; }
        [DataMember]
        public decimal Summa { get; set; }
        [DataMember]
        public string Status { get; set; }
    }
}
