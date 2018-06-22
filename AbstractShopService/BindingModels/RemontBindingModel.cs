using System.Collections.Generic;
using System.Runtime.Serialization;
namespace AbstractWorkService.BindingModels
{
    [DataContract]
    public class RemontBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string RemontName { get; set; }
        [DataMember]
        public decimal Cost { get; set; }
        [DataMember]
        public List<RemontMaterialBindingModel> RemontMaterials { get; set; }
    }
}
