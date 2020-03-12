using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.Model
{
    [DataContract]
    public class PartList
    {
        

        [DataMember(Name = "dealerNumber")]
        // Dealerid as salesforceid
        [JsonPropertyName("dealerNumber")] public string dealerNumber { get; set; }
        [JsonPropertyName("SalesForceID")] public string SalesForceID { get; set; }
        [JsonPropertyName("invoiceNumber")] [DataMember(Name = "invoiceNumber")] public string invoiceNumber  { get; set; }
        [JsonPropertyName("materialNumber")] [DataMember(Name = "materialNumber")] public string materialNumber  { get; set; }
        [JsonPropertyName("buName")] [DataMember(Name = "buName")] public string buName { get; set; }

}
    [DataContract]
    public class PartPrice
    {

        [DataMember(Name = "dealerNumber")]
        public string dealerNumber { get; set; }
        [DataMember(Name = "invoiceNumber")]
        public string invoiceNumber { get; set; }

        //[DataMember(Name = "SalesForceID")] public string SalesForceID { get; set; }
        [DataMember(Name = "materialNumber")] public string materialNumber { get; set; }
        [DataMember(Name = "buName")] public string buName { get; set; }
        [DataMember(Name = "unitPrice")] public string unitPrice { get; set; }

    }
    public class GetToken
    {
        [DataMember(Name = "ClientId")] public string ClientId { get; set; }
        [DataMember(Name = "ClientSecret")] public string ClientSecret { get; set; }
        [DataMember(Name = "Scope")] public string Scope { get; set; }
    }
    public class tbl_product
    {
        public int ProductID { get; set; }
        public string Name  { get; set; }
		   public string ProductNumber  { get; set; }
		   public string Price  { get; set; }
		   public string Quantity  { get; set; }
		   public string Color  { get; set; }
}
}
