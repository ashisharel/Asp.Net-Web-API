using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace LibertyWebAPI.Utilities
{
    public static class Helper
    {
        public static void ParseErrorMessage(string errorMessage, out string message, out string code, char delimiter = '|')
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                message = string.Empty;
                code = string.Empty;
                return;
            }
            
            var splitMsg = errorMessage.Split(delimiter);
            if (Regex.IsMatch(splitMsg[0], @"^\d+$"))
            { 
                code = "LIB" + splitMsg[0].Trim();
                message = splitMsg.Length > 1 ? splitMsg[1] : errorMessage;
            }
            else
            {
                code = "LIB1005";
                message = errorMessage;
            }
            
        }
    }
}