using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DataModel.Contracts;
using LibertyWebAPI.DTO.Product;
using System.Collections.Generic;
using System.Linq;

namespace LibertyWebAPI.BusinessServices
{
    public class AccessoryCategoryService : IAccessoryCategoryService
    {
        private readonly IAccessoryCategoryRepository _accessoryCategoryRepository;

        public AccessoryCategoryService(IAccessoryCategoryRepository accessoryCategoryRepository)
        {
            _accessoryCategoryRepository = accessoryCategoryRepository;
        }

        public AccessoryCategoryDTO GetProductAccents(string productId, string categoryId, int sessionId)
        {
            var accessoryCategory = _accessoryCategoryRepository.GetProductAccents(productId, categoryId, sessionId);
            if (accessoryCategory != null)
            {
                return MapAccentsDTO(accessoryCategory, categoryId);
            }
            return null;
        }

        public AccessoryCategoryDTO GetProductBackgrounds(string productId, string categoryId, int sessionId)
        {
            var accessoryCategory = _accessoryCategoryRepository.GetProductBackgrounds(productId, categoryId, sessionId);
            if (accessoryCategory != null)
            {
                return MapAccentsDTO(accessoryCategory, categoryId);
            }
            return null;
        }

        public AccessoryCategoryDTO GetProductOneliners(string productId, int sessionId)
        {
            var accessoryCategory = _accessoryCategoryRepository.GetProductOneliners(productId, sessionId);
            if (accessoryCategory != null)
            {
                return MapOnelinersDTO(accessoryCategory);
            }
            return null;
        }

        public AccessoryCategoryDTO MapAccentsDTO(AccessoryCategory accessoryCategory, string categoryId)
        {
            var dto = new AccessoryCategoryDTO() { Name = accessoryCategory.Name, Code = accessoryCategory.Code };

            // only categories; no Items/Accents
            if (categoryId == "null")
            {
                foreach (var level1 in accessoryCategory.Groups)
                {
                    var gpo1 = new AccessoryCategoryDTO() { Code = level1.Code, Name = level1.Name };
                    dto.Groups.Add(gpo1);
                }
                return dto;
            }

            // all categories, including Items/Accents
            if (categoryId == "all")
            {
                foreach (var level1 in accessoryCategory.Groups)
                {
                    var gpo1 = new AccessoryCategoryDTO() { Code = level1.Code, Name = level1.Name, Groups = null, Items = new List<AccessoryDetailsDTO>() };
                    foreach (var level2 in level1.Groups)
                    {
                        foreach (var level3 in level2.Groups)
                        {
                            var items = level3.Items.Select(i => new AccessoryDetailsDTO()
                            {
                                Code = i.Code,
                                Name = i.Name,
                                Pricing = null,
                                Type = i.Type.Equals("E") ? "A" : i.Type,
                                Url = i.Url
                            }).ToList();
                            gpo1.Items = gpo1.Items.Union(items).ToList();
                        }
                        gpo1.Items = gpo1.Items.OrderBy(i => i.Name).ThenBy(i => i.Code).ToList();
                    }
                    dto.Groups.Add(gpo1);
                }
                return dto;
            }

            // only one category and its Items/Accents
            var category = accessoryCategory.Groups.FirstOrDefault(g => g.Code == categoryId);

            if (category != null)
            {
                var gpo1 = new AccessoryCategoryDTO() { Code = category.Code, Name = category.Name, Groups = null, Items = new List<AccessoryDetailsDTO>() };

                foreach (var level2 in category.Groups)
                {
                    foreach (var level3 in level2.Groups)
                    {
                        var items = level3.Items.Select(i => new AccessoryDetailsDTO()
                        {
                            Code = i.Code,
                            Name = i.Name,
                            Pricing = null,
                            Type = i.Type.Equals("E") ? "A" : i.Type,
                            Url = i.Url
                        }).ToList();

                        gpo1.Items = gpo1.Items.Union(items).ToList();
                    }
                }
                // sort Items by name and then by code
                gpo1.Items = gpo1.Items.OrderBy(i => i.Name).ThenBy(i => i.Code).ToList();
                dto.Groups.Add(gpo1);
                return dto;
            }
            return null;
        }

        public AccessoryCategoryDTO MapOnelinersDTO(AccessoryCategory accessoryCategory)
        {
            var dto = new AccessoryCategoryDTO() { Name = accessoryCategory.Name, Code = accessoryCategory.Code };
            foreach (var level1 in accessoryCategory.Groups)
            {
                foreach (var level2 in level1.Groups)
                {
                    foreach (var level3 in level2.Groups)
                    {
                        var gpo3 = new AccessoryCategoryDTO() { Code = level3.Code, Name = level3.Name, Groups = null };
                        gpo3.Items = level3.Items.Select(i => new AccessoryDetailsDTO()
                        {
                            Code = i.Code,
                            Name = i.Name,
                            Pricing = null,
                            Type = i.Type,
                            Url = i.Url
                        }).OrderBy(i => i.Name).ToList();

                        dto.Groups.Add(gpo3);
                    }
                }
            }
            return dto;
        }
    }
}