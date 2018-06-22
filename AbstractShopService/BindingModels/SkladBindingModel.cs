using System.Runtime.Serialization;
namespace AbstractWorkService.BindingModels
{
    [DataContract]
    public class SkladBindingModel
    {

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string SkladName { get; set; }
    }
}
