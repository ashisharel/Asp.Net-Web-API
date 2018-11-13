using System.Runtime.Serialization;

namespace LibertyWebAPI.DTO.FOA
{
    public class FOASaveDTO
    {        
        [IgnoreDataMember]
        public string RtNumber { get; set; }
        [IgnoreDataMember]
        public string AccountNumber { get; set; }
        public string FirstName { get; set; }
        public string Initial { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string DOB { get; set; }
        public string QuestionCount { get; set; }
        public string Result { get; set; }
        [IgnoreDataMember]
        public string SourceSystem { get; set; } // -- '4' = LOO, '9' = Wincheck (Order_Source codes)
        public string StartCheckNumber { get; set; }
    }
}
