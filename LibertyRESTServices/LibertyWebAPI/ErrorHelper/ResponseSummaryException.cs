using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text.RegularExpressions;

namespace LibertyWebAPI.ErrorHelper
{
    /// <summary>
    /// PeP Handle exception interface
    /// </summary>
    public interface IResponseSummaryException
    {
        /// <summary>
        /// the response status, either "SUCCESS" or "FAILURE"
        /// </summary>
        string Status { get; set; }

        /// <summary>
        /// list of messages to to send back to the request
        /// </summary>
        List<Message> Messages { get; set; }
    }

    
    /// <summary>
    /// ResponseSummaryException
    /// </summary>
    [Serializable]
    public class ResponseSummaryException : Exception, IResponseSummaryException, ISerializable
    {
        /// <summary>
        /// the response status, either "SUCCESS" or "FAILURE"
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// list of messages to to send back to the request
        /// </summary>
        public List<Message> Messages { get; set; }

        /// <summary>
        /// Populates with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo to populate with data.</param>
        /// <param name="context">The destination (see System.Runtime.Serialization.StreamingContext) for this serialization.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }

    /// <summary>
    /// the message to send back to the request
    /// </summary>
    public class Message
    {
        /// <summary>
        /// description of the message; internally used.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// code of the message i.e. LIB1003
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// message text displayable to the client
        /// </summary>
        public string Text { get; set; }
    }

    /// <summary>
    /// Invalid liberty sessionId exception
    /// </summary>
    [Serializable]
    public class InvalidSessionIdException : ResponseSummaryException
    {
        /// <summary>
        /// constructor
        /// </summary>
        public InvalidSessionIdException()
        {
            Status = "FAILURE";
            Messages = new List<Message>()
            {
                new Message()
                {
                    Code = "LIB1003",
                    Description = "SessionId must be numeric; can't be null or empty.",
                    Text = "We are sorry. There is a problem with that product online. It’s our fault. We recommend that you choose another product. Alternatively, you may contact our customer support at 1-877-585-8777 or you may contact your financial institution to order this item."
                }
            };
        }
    }

    /// <summary>
    /// Invalid liberty sessionId exception
    /// </summary>
    [Serializable]
    public class InvalidZipcodeException : ResponseSummaryException
    {
        /// <summary>
        /// constructor
        /// </summary>
        public InvalidZipcodeException(string errorCode, string message)
        {
            Status = "FAILURE";
            Messages = new List<Message>()
            {
                new Message()
                {
                    Code = "LIB11001", //TBD: remove one zero
                    Description = errorCode + " - Invalid logon zip.",
                    Text = message
                }
            };
        }
    }

    /// <summary>
    /// Item(s) not found in Liberty's database exception
    /// </summary>
    [Serializable]
    public class FailureException : ResponseSummaryException
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="message">the error message</param>
        public FailureException(string message)
        {
            Status = "FAILURE";
            Messages = new List<Message>()
            {
                new Message()
                {
                    Code = "LIB1002",
                    Description = message,
                    Text = !string.IsNullOrWhiteSpace(message) ? message :
                    "We are sorry. There is a problem with that product online. It’s our fault. We recommend that you choose another product. Alternatively, you may contact our customer support at 1-877-585-8777 or you may contact your financial institution to order this item."
                }
            };
        }
    }

    /// <summary>
    /// Validation exception
    /// </summary>
    [Serializable]
    public class ValidationException : ResponseSummaryException
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="messages">list of the error messages</param>
        public ValidationException(List<Message> messages)
        {
            Status = "FAILURE";
            Messages = messages;
            Messages.ForEach(x => x.Text = !string.IsNullOrWhiteSpace(x.Text) ? x.Text : 
                "We are sorry. There is a problem with that product online. It’s our fault. We recommend that you choose another product. Alternatively, you may contact our customer support at 1-877-585-8777 or you may contact your financial institution to order this item.");
        
        }
        
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="message">the error message</param>
        public ValidationException(string message)
        {
            Status = "FAILURE";
            Messages = new List<Message>()
            {
                new Message()
                {
                    Code = "LIB1003",
                    Description = message,
                    Text = !string.IsNullOrWhiteSpace(message) ? message :
                    "We are sorry. There is a problem with that product online. It’s our fault. We recommend that you choose another product. Alternatively, you may contact our customer support at 1-877-585-8777 or you may contact your financial institution to order this item."
                }
            };
        }
    }

    /// <summary>
    /// Unhandled Exception
    /// </summary>
    [Serializable]
    public class UnhandledException : ResponseSummaryException
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="ex">the source of the exception</param>
        public UnhandledException(Exception ex)
        {
            Status = "FAILURE";
            var errorCode = !string.IsNullOrWhiteSpace(ex.Message) && ex.Message.Split('|').Length > 0 ? ex.Message.Split('|')[0] : "";
            var errMsg = string.Empty;
            switch (errorCode)
            {
                case "30004":
                case "30005":
                case "30007":
                case "30008":
                case "30009":
                    {
                        errMsg = ex.Message.Split('|')[1];
                        break;
                    }
                case "10001":
                    {
                        errMsg = "We're sorry your account cannot be ordered online at this time. Please call Customer Service at 1-800-350-0971 or contact your financial institution. We apologize for any inconvenience.";
                        break;
                    }
                default:
                    errMsg = "We are sorry. There is a problem with that product online. It’s our fault. We recommend that you choose another product. Alternatively, you may contact our customer support at 1-877-585-8777 or you may contact your financial institution to order this item.";
                    break;
            }
            Messages = new List<Message>();
            do
            {
                Messages.Add(new Message()
                {
                    Code = !string.IsNullOrWhiteSpace(errorCode) && Regex.IsMatch(errorCode, @"^\d+$") ? "LIB" + errorCode : "LIB1005",
                    Description = ex.Message,
                    Text = errMsg                    
                });

                ex = ex.InnerException;
            } while (ex != null);
        }
    }
}