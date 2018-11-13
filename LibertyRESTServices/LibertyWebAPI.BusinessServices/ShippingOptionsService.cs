using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DataModel.Contracts;
using LibertyWebAPI.DTO.Common;
using LibertyWebAPI.DTO.Order;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LibertyWebAPI.BusinessServices
{
    public class ShippingOptionsService : IShippingOptionsService
    {
        private readonly IShippingOptionsRepository _shippingOptionsRepository;
        public ShippingOptionsService(IShippingOptionsRepository shippingOptionsRepository)
        {
            _shippingOptionsRepository = shippingOptionsRepository;
        }
        public IEnumerable<ShippingOptionDTO> GetShippingOptions(ShippingOptionsRequestDTO shippingOptionsRequest, int sessionId)
        {
            string[] addressLines = { shippingOptionsRequest.ShippingLine1 = string.IsNullOrWhiteSpace(shippingOptionsRequest.ShippingLine1) ? string.Empty : shippingOptionsRequest.ShippingLine1, 
                                        shippingOptionsRequest.ShippingLine2 = string.IsNullOrWhiteSpace(shippingOptionsRequest.ShippingLine2) ? string.Empty : shippingOptionsRequest.ShippingLine2,
                                        shippingOptionsRequest.ShippingLine3 = string.IsNullOrWhiteSpace(shippingOptionsRequest.ShippingLine3) ? string.Empty : shippingOptionsRequest.ShippingLine3,
                                        shippingOptionsRequest.ShippingLine4 = string.IsNullOrWhiteSpace(shippingOptionsRequest.ShippingLine4) ? string.Empty : shippingOptionsRequest.ShippingLine4,
                                        shippingOptionsRequest.ShippingLine5 = string.IsNullOrWhiteSpace(shippingOptionsRequest.ShippingLine5) ? string.Empty : shippingOptionsRequest.ShippingLine5
                                    };
            shippingOptionsRequest.IsPOBox = isPOBoxAddress(addressLines);
            var shippingOptions = _shippingOptionsRepository.GetShippingOptions(shippingOptionsRequest, sessionId);
            if (shippingOptions != null && shippingOptions.Any())
            {
                return MapDTO(shippingOptions);
            }
            return null;
        }

        public IEnumerable<ShippingOptionDTO> MapDTO(IEnumerable<BusinessEntities.ShippingOption> shippingOptions)
        {
            var shippingOptionsDTO = new List<ShippingOptionDTO>();
            foreach(var option in shippingOptions)
            {
                var optionDTO = new ShippingOptionDTO()
                {
                    Code = option.Code,
                    Description = option.Description,
                    EstimatedDelivery = Convert.ToString(option.EstimatedDelivery),
                    Fee = new MoneyDTO()
                    {
                        Amount = option.Fee
                    },
                    Name = option.Name,
                    Note = option.Note,
                    IsPreselected = option.IsPreselected
                };
                shippingOptionsDTO.Add(optionDTO);
            }
            return shippingOptionsDTO;
        }

        /// <summary>
        /// LOO code, returns true if given address is a PO Box
        /// </summary>
        /// <param name="addressLines"></param>
        /// <returns></returns>
        private static bool isPOBoxAddress(string[] addressLines)
        {
            bool POBoxFound = false;
            Regex POBoxRegex = new Regex("(^|\\s)P\\.?(\\s?|\\s+)O\\.?(\\s?|\\s+)(BOX:?)?(\\s?|\\s+)(\\#?)(\\s?|\\s+)(\\d+)", RegexOptions.IgnoreCase);
            Regex POBoxLetterRegex = new Regex("(^|\\s)P\\.?(\\s?|\\s+)O\\.?(\\s?|\\s+)(BOX:?)(\\s?|\\s+)(\\#?)(\\s?|\\s+)([a-z]+)", RegexOptions.IgnoreCase);
            foreach (string line in addressLines)
            {
                if (POBoxRegex.Match(line).Success || POBoxLetterRegex.Match(line).Success)
                {
                    POBoxFound = true;
                }
            }
            return POBoxFound;
        }
    }
}
