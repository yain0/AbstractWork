using System.Runtime.Serialization;
namespace AbstractWorkService.BindingModels
{
    [DataContract]
    public class SkladMaterialBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int SkladId { get; set; }
        [DataMember]
        public int MaterialId { get; set; }
        [DataMember]
        public int Koll { get; set; }
    }
}
