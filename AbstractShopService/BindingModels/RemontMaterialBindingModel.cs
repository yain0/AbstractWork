using System.Runtime.Serialization;
namespace AbstractWorkService.BindingModels
{
    [DataContract]
    public class RemontMaterialBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int RemontId { get; set; }
        [DataMember]
        public int MaterialId { get; set; }
        [DataMember]
        public int Koll { get; set; }
    }
}
