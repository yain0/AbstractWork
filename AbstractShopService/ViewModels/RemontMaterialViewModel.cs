using System.Runtime.Serialization;
namespace AbstractWorkService.ViewModels
{
    [DataContract]
    public class RemontMaterialViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int RemontId { get; set; }
        [DataMember]
        public int MaterialId { get; set; }
        [DataMember]
        public string MaterialName { get; set; }
        [DataMember]
        public int Koll { get; set; }
    }
}
