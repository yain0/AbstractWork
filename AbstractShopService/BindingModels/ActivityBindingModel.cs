
using System.Runtime.Serialization;
namespace AbstractWorkService.BindingModels
{
    [DataContract]
    public class ActivityBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int CustomerId { get; set; }
        [DataMember]
        public int RemontId { get; set; }
        [DataMember]
        public int? WorkerId { get; set; }
        [DataMember]
        public int Koll { get; set; }
        [DataMember]
        public decimal Summa { get; set; }
    }
}
