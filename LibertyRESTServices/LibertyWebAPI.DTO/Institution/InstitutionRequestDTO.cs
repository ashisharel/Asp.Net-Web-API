using LibertyWebAPI.DTO.FOA;
using System.ComponentModel.DataAnnotations;

namespace LibertyWebAPI.DTO.Institution
{
    /// <summary>
    /// Institution request parameter
    /// </summary>
    public class InstitutionRequestDTO
    {
        //[Required]
        /// <summary>
        /// The transit routing number of the institution e.g. '256074974'
        /// </summary>
        public string RtNumber { get; set; }

        //[Required]
        /// <summary>
        /// The account of the customer requesting the institution information.
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// The Integration payload coming via Integrated Login
        /// </summary>
        public string Payload { get; set; }

        /// <summary>
        /// True if Business selected in OMC login page
        /// </summary>
        public bool? IsBusiness { get; set; }

        /// <summary>
        /// indicates whether this is a an FOA or not
        /// </summary>
        public bool? FOAFlag { get; set; }

        /// <summary>
        /// Integrated logins/FOA.  If provided, send rtNumber accountNumber as empty/null
        /// the FOASession returned from the call to /api/foa/result
        /// </summary>
        public string PreAuthSession { get; set; }

        /// <summary>
        /// from the selected Branch in the List from /api/foa/branches
        /// </summary>
        public string FOAAbaBrId { get; set; }
        
        /// <summary>
        /// from the first call to the institution
        /// </summary>
        public string FOAAcctType { get; set; }

        //FOASaveDTO merged in InstitutionRequestDTO
        public FOASaveDTO FOAResult { get; set; }

        /// <summary>
        /// Zipcode where the last order was shipped
        /// </summary>
        public int LogonZip { get; set; }
        
    }
}