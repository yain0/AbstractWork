using System.Runtime.Serialization;
using System.Collections.Generic;

namespace AbstractWorkService.ViewModels
{
    [DataContract]
    public class SkladViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string SkladName { get; set; }
        [DataMember]
        public List<SkladMaterialViewModel> SkladMaterials { get; set; }
    }
}
