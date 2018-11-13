using System.Collections.Generic;

namespace LibertyWebAPI.DTO.Common
{
    public class ResponseDTO
    {
        public string Status { get; set; }
        public IList<MessageDTO> Messages { get; set; }
    }

    public class MessageDTO
    {
        public string Description { get; set; }
        public string Code { get; set; }
        public string Text { get; set; }
    }
}