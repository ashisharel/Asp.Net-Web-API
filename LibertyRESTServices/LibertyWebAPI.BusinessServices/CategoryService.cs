using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DataModel.Contracts;
using LibertyWebAPI.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibertyWebAPI.BusinessServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<CategoryItemsDTO> GetCategoryItems(CategoryRequestDTO categoryRequest, int sessionId)
        {
            var categoryData = _categoryRepository.GetCategoryData(categoryRequest, sessionId);
            if (categoryData != null && categoryData.Any())
            {
                return MapDTO(categoryData);
            }
            return null;
        }

        public IEnumerable<CategoryItemsDTO> MapDTO(IEnumerable<Category> categoryItems)
        {
            var categoryItemsDTO = new List<CategoryItemsDTO>();            

            foreach (Category item in categoryItems)
            {
                var categoryItem = new CategoryItemsDTO()
                {
                    LibertyProductId = Convert.ToString(item.ProductID),
                    Name = item.ProductName,                    
                    Image = item.ImageURL, // TODO : temporary fix
                    ProductType = item.ProductType,
                    Price = item.Price,
                    CheckScenes = item.Check_Scenes,
                    HcProductId = item.HarlandProductId,
                    StyleId = item.StyleId,
                    Attribute = item.Attribute,
                    CategoryName = item.CategoryName,
                    PopularityScore = item.PopularityScore,
                };
                categoryItemsDTO.Add(categoryItem);
            }

            return categoryItemsDTO;
        }
    }
}
