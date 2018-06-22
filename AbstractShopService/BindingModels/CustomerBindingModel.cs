﻿using System.Runtime.Serialization;

namespace AbstractWorkService.BindingModels
{
    [DataContract]
    public class CustomerBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Mail { get; set; }

        [DataMember]
        public string CustomerFIO { get; set; }
    }
}
