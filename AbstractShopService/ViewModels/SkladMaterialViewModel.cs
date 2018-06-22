using System.Runtime.Serialization;
namespace AbstractWorkService.ViewModels
{
    [DataContract]
    public class SkladMaterialViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int SkladId { get; set; }
        [DataMember]
        public int MaterialId { get; set; }
        [DataMember]
        public string MaterialName { get; set; }
        [DataMember]
        public int Koll { get; set; }
    }
}
