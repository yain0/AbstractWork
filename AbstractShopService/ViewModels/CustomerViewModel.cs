﻿using System.Collections.Generic;
using System.Runtime.Serialization;
namespace AbstractWorkService.ViewModels
{
    [DataContract]
    public class CustomerViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Mail { get; set; }

        [DataMember]
        public string CustomerFIO { get; set; }

        [DataMember]
        public List<MessageInfoViewModel> Messages { get; set; }
    }
}
