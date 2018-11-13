using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Linq;
using System.Runtime.Serialization.Formatters;


namespace LibertyWebAPI.Utilities
{
    public static class JSONHelper
    {
        #region Public extension methods.
        /// <summary>
        /// Extened method of object class, Converts an object to a json string.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJSON(this object obj)
        {            
            try
            {
                var serializer = new JavaScriptSerializer();
                return serializer.Serialize(obj);                
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion
    }
    
}