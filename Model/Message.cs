using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Api.Model
{
    [DataContract]
    public class Message<T>
    {
        [DataMember(Name = "IsSuccess")]
        public bool IsSuccess { get; set; }

        [DataMember(Name = "ReturnMessage")]
        public string ReturnMessage { get; set; }

        [DataMember(Name = "Error")]
        public string Error { get; set; }
        [DataMember(Name = "StatusCode")]
        public string StatusCode { get; set; }

        [DataMember(Name = "Data")]
        public List<T> Data { get; set; }
    }
}
