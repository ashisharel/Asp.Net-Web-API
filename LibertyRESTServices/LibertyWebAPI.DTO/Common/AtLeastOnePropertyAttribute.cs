using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace LibertyWebAPI.DTO.Common
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AtLeastOnePropertyAttribute : ValidationAttribute
    {
        private string[] PropertyList { get; set; }

        public AtLeastOnePropertyAttribute(params string[] propertyList)
        {
            this.PropertyList = propertyList;
        }

        public override bool IsValid(object value)
        {
            if (null == value) return true;

            PropertyInfo propertyInfo;
            foreach (string propertyName in PropertyList)
            {
                propertyInfo = value.GetType().GetProperty(propertyName);
                var propValue = propertyInfo.GetValue(value, null);

                if (propertyInfo != null && propValue != null && !string.IsNullOrWhiteSpace(propValue.ToString()))
                {
                    return true;
                }
            }

            return false;
        }
    }
}