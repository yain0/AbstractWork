using System.Runtime.Serialization;
namespace AbstractWorkService.ViewModels
{
    [DataContract]
    public class ActivityViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CustomerId { get; set; }
        [DataMember]
        public string CustomerFIO { get; set; }
        [DataMember]
        public int RemontId { get; set; }
        [DataMember]
        public string RemontName { get; set; }
        [DataMember]
        public int? WorkerId { get; set; }
        [DataMember]
        public string WorkerName { get; set; }
        [DataMember]
        public int Koll { get; set; }
        [DataMember]
        public decimal Summa { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string DateCreate { get; set; }
        [DataMember]
        public string DateWork { get; set; }
    }
}
