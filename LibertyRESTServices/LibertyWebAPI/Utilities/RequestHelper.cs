using LibertyWebAPI.DTO.Common;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;

namespace LibertyWebAPI.Utilities
{
    /// <summary>
    /// "Request" utilities
    /// </summary>
    public class RequestHelper
    {
        /// <summary>
        /// Returns the "SessionId" value from the Request.Headers; otherwise returns -1
        /// </summary>
        /// <param name="header">The Request.Headers object</param>
        /// <returns>integer</returns>
        public static int GetSessionIdFromHeader(HttpRequestHeaders header)
        {
            int sessionId = -1;
            if (header.Contains("SessionId"))
            {
                var hdrSessionId = header.GetValues("SessionId").First();
                if (!int.TryParse(hdrSessionId, out sessionId))
                    sessionId = -1;
            }
            else
                sessionId = -1;
            return sessionId;
        }

        /// <summary>
        /// InvalidSessionIdResponse
        /// </summary>
        public static ResponseDTO InvalidSessionIdResponse()
        {
            return CreateBadRequestResponse("SessionId must be numeric; can't be null or empty.");
        }

        /// <summary>
        /// CreateBadRequestResponse
        /// </summary>
        /// <param name="errorMessage"></param>
        public static ResponseDTO CreateBadRequestResponse(string errorMessage)
        {
            return CreateGenericResponse("LIB1003", errorMessage);
        }

        /// <summary>
        /// CreateNotFoundResponse
        /// </summary>
        /// <param name="errorMessage"></param>
        public static ResponseDTO CreateNotFoundResponse(string errorMessage)
        {
            return CreateGenericResponse("LIB1002", errorMessage);
        }

        /// <summary>
        /// CreateGenericResponse
        /// </summary>
        /// <param name="code"></param>
        /// <param name="errorId"></param>
        /// <param name="errorMessage"></param>
        public static ResponseDTO CreateGenericResponse(string errorId, string errorMessage)
        {
            return new ResponseDTO()
            {
                Status = "FAILURE",
                Messages = new List<MessageDTO>()
                {
                    new MessageDTO()
                    {
                        Code = errorId,
                        Description = errorMessage,
                        Text = errorMessage
                    }
                }
            };
        }

        /// <summary>
        /// CreateGenericResponse
        /// </summary>
        /// <param name="errorId"></param>
        /// <param name="errorMessage"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static ResponseDTO CreateGenericResponse(string errorId, string errorMessage, string description)
        {
            return new ResponseDTO()
            {
                Status = "FAILURE",
                Messages = new List<MessageDTO>()
                {
                    new MessageDTO()
                    {
                        Code = errorId,
                        Description = description,
                        Text = errorMessage
                    }
                }
            };
        }

        internal static int? TryParse2(string s)
        {
            int i;
            if (!int.TryParse(s, out i))
            {
                return null;
            }
            else
            {
                return i;
            }
        }

    }
}