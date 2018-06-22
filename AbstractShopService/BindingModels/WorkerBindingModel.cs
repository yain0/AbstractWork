using System.Runtime.Serialization;
namespace AbstractWorkService.BindingModels
{
    [DataContract]
    public class WorkerBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string WorkerFIO { get; set; }
    }
}
