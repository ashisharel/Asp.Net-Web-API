namespace LibertyWebAPI.DTO.FOA
{
    public class BranchRequestDTO
    {
        /// <summary>
        /// The transit routing number of the institution e.g. '256074974'
        /// </summary>
        public string RtNumber { get; set; } 

        /// <summary>
        /// The account of the customer requesting the institution information.
        /// </summary>
        public string AccountNumber { get; set; }
    }
}