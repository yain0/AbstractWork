using System.Runtime.Serialization;
using System.Collections.Generic;

namespace AbstractWorkService.ViewModels
{
    [DataContract]
    public class RemontViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string RemontName { get; set; }
        [DataMember]
        public decimal Cost { get; set; }
        [DataMember]
        public List<RemontMaterialViewModel> RemontMaterials { get; set; }
    }
}
