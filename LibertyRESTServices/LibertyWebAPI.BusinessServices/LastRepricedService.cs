using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DataModel.Contracts;
using LibertyWebAPI.DTO.Common;
using LibertyWebAPI.DTO.Order;
using System;

namespace LibertyWebAPI.BusinessServices
{
    public class LastRepricedService : ILastRepricedService
    {
        public readonly ILastRepricedRepository _orderRepository;
        public LastRepricedService(ILastRepricedRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public OrderDTO GetLastRepriced(int sessionId)
        {
            var objOrder = _orderRepository.GetLastRepriced(sessionId);
            if (objOrder != null)
            {
                return MapDTO(objOrder);
            }
            return null;
        }

        public OrderDTO MapDTO(Order order)
        {
            char[] lineRule = null;
            if (!string.IsNullOrEmpty(order.OrderItems[0].FontLine))
            {
                lineRule = order.OrderItems[0].FontLine.Trim().ToCharArray();
            }

            var orderDTO = new OrderDTO
            {
                Customer = new CustomerDTO
                {
                    EmailAddress = order.EmailAddress,
                    Name = String.IsNullOrWhiteSpace(order.Customername) ? null : order.Customername.Trim(),
                    Telephone = new TelephoneDTO
                    {
                        Home = order.Telephone // defaulting all telephone to home and not business
                    }
                },
                PlacedOn = order.OrderDate,
                //placedRecent = //TODO
                ShippingAddress = new AddressDTO
                {
                    ShipLine1 = String.IsNullOrWhiteSpace(order.ShippingAddress.ShippingLine1) ? null : order.ShippingAddress.ShippingLine1.Trim(),
                    ShipLine2 = String.IsNullOrWhiteSpace(order.ShippingAddress.ShippingLine2) ? null : order.ShippingAddress.ShippingLine2.Trim(),
                    ShipLine3 = String.IsNullOrWhiteSpace(order.ShippingAddress.ShippingLine3) ? null : order.ShippingAddress.ShippingLine3.Trim(),
                    ShipLine4 = String.IsNullOrWhiteSpace(order.ShippingAddress.ShippingLine4) ? null : order.ShippingAddress.ShippingLine4.Trim(),
                    ShipLine5 = String.IsNullOrWhiteSpace(order.ShippingAddress.ShippingLine5) ? null : order.ShippingAddress.ShippingLine5.Trim(),
                    City = String.IsNullOrWhiteSpace(order.ShippingAddress.City) ? null : order.ShippingAddress.City.Trim(),
                    State = String.IsNullOrWhiteSpace(order.ShippingAddress.State) ? null : order.ShippingAddress.State.Trim(),
                    PostalCode = String.IsNullOrWhiteSpace(order.ShippingAddress.Zipcode) ? null : order.ShippingAddress.Zipcode.Trim(),
                    IsForeign = order.ShippingAddress.IsForeign
                },
            };

            if (order.OrderItems != null && order.OrderItems.Count > 0)
            {
                var item = new OrderItemDTO()
                {
                    FITotal = new MoneyDTO()
                    {
                        Amount = order.OrderItems[0].FITotal/100.00
                    },
                    Check = new CheckDTO()
                    {
                        Accent = new AccessoryDTO()
                        {
                            Code = string.IsNullOrWhiteSpace(order.OrderItems[0].Accent.Id) ? null : order.OrderItems[0].Accent.Id.Trim(),
                            Name = order.OrderItems[0].Accent.Name,
                            Preselected = order.OrderItems[0].Accent.Preselected,
                            Removable = order.OrderItems[0].Accent.Removable,
                            Price = new MoneyDTO()
                            {
                                Amount = order.OrderItems[0].Accent.Price
                            },
                            Url = order.OrderItems[0].Accent.Url
                        },
                        Background = new AccessoryDTO()
                        {
                            Code = string.IsNullOrWhiteSpace(order.OrderItems[0].Phantom.Id) ? null : order.OrderItems[0].Phantom.Id.Trim(),
                            Name = order.OrderItems[0].Phantom.Name,
                            Preselected = order.OrderItems[0].Phantom.Preselected,
                            Removable = order.OrderItems[0].Phantom.Removable,
                            Price = new MoneyDTO()
                            {
                                Amount = order.OrderItems[0].Phantom.Price
                            },
                            Url = order.OrderItems[0].Phantom.Url
                        },
                        Font = new AccessoryDTO()
                        {
                            Code = string.IsNullOrWhiteSpace(order.OrderItems[0].Font.Id) ? null : order.OrderItems[0].Font.Id.Trim(),
                            Name = order.OrderItems[0].Font.Name,
                            Preselected = order.OrderItems[0].Font.Preselected,
                            Removable = order.OrderItems[0].Font.Removable,
                            Price = new MoneyDTO()
                            {
                                Amount = order.OrderItems[0].Font.Price
                            }
                        },
                        FraudArmor = new AccessoryDTO()
                        {
                            Code = string.IsNullOrWhiteSpace(order.OrderItems[0].FraudArmor.Id) ? null : order.OrderItems[0].FraudArmor.Id.Trim(),
                            Name = order.OrderItems[0].FraudArmor.Name,
                            Preselected = order.OrderItems[0].FraudArmor.Preselected,
                            Removable = order.OrderItems[0].FraudArmor.Removable,
                            Price = new MoneyDTO()
                            {
                                Amount = order.OrderItems[0].FraudArmor.Price
                            }
                        },
                        OneLiner = new AccessoryDTO()
                        {
                            Code = string.IsNullOrWhiteSpace(order.OrderItems[0].SigCut.Id) ? null : order.OrderItems[0].SigCut.Id.Trim(),
                            Name = order.OrderItems[0].SigCut.Name,
                            Preselected = order.OrderItems[0].SigCut.Preselected,
                            Removable = order.OrderItems[0].SigCut.Removable,
                            Price = new MoneyDTO()
                            {
                                Amount = order.OrderItems[0].SigCut.Price
                            },
                            Url = order.OrderItems[0].SigCut.Url
                        },
                        Price = new MoneyDTO()
                        {
                            Amount = order.OrderItems[0].ItemSubTotal
                        },
                        ProductId = string.IsNullOrWhiteSpace(order.OrderItems[0].ProductId) ? null : order.OrderItems[0].ProductId.Trim(),
                        Quantity = new QuantityDTO()
                        {
                            Amount = order.OrderItems[0].Quantity,
                            Unit = "checks",
                        },
                        StartAt = order.OrderItems[0].StartingCheckNumber,
                        Color = order.OrderItems[0].ProductColor,
                        OverSignature = new string[] { order.OrderItems[0].SigLine1, order.OrderItems[0].SigLine2 },
                        TitlePlateLogo = string.IsNullOrWhiteSpace(order.OrderItems[0].TitlePlateLogo) ? null : order.OrderItems[0].TitlePlateLogo.Trim(),
                        Personalization = new PersonalizationDTO()
                        {
                            PersLine1 = new PersonalizationLineDTO()
                            {
                                Text = order.OrderItems[0].Personalization.PersonalizationLine1,
                                IsBold = lineRule.Length >= 1 && lineRule[0] == 'N' ? true : false
                            },
                            PersLine2 = new PersonalizationLineDTO()
                            {
                                Text = order.OrderItems[0].Personalization.PersonalizationLine2,
                                IsBold = lineRule.Length >= 2 && lineRule[1] == 'N' ? true : false
                            },
                            PersLine3 = new PersonalizationLineDTO()
                            {
                                Text = order.OrderItems[0].Personalization.PersonalizationLine3,
                                IsBold = lineRule.Length >= 3 && lineRule[2] == 'N' ? true : false
                            },
                            PersLine4 = new PersonalizationLineDTO()
                            {
                                Text = order.OrderItems[0].Personalization.PersonalizationLine4,
                                IsBold = null
                            },
                            PersLine5 = new PersonalizationLineDTO()
                            {
                                Text = order.OrderItems[0].Personalization.PersonalizationLine5,
                                IsBold = null
                            },
                            PersLine6 = new PersonalizationLineDTO()
                            {
                                Text = order.OrderItems[0].Personalization.PersonalizationLine6,
                                IsBold = null
                            }
                        },
                        ShippingOption = new ShippingOptionDTO()
                        {
                            Code = order.OrderItems[0].ShippingOption.Code,
                            Name = order.OrderItems[0].ShippingOption.Name,
                            Fee = new MoneyDTO
                            {
                                Amount = order.OrderItems[0].ShippingOption.Fee
                            }
                        }
                    }
                };
                            
                orderDTO.Items.Add(item);
            }
            return orderDTO;
        }
    }
}
