using System.Collections.Generic;

namespace LibertyWebAPI.DTO.Product
{
    /// <summary>
    /// Get accent symbols for the indicated product, offered to the customer.
    /// </summary>
    public class AccessoryCategoryDTO
    {
        public AccessoryCategoryDTO()
        {
            Groups = new List<AccessoryCategoryDTO>();
            //items = new List<AccessoryDetailsDTO>();
        }

        /// <summary>
        /// This is the name of the category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// This is the code of the category.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// A set of AccessoryCategory, if this category has sub-categories.
        /// </summary>
        public IList<AccessoryCategoryDTO> Groups { get; set; }
        
        /// <summary>
        /// A set of AccessoryDetails, if this category has accessories (accents or one-liners)
        /// </summary>
        public IList<AccessoryDetailsDTO> Items { get; set; }
    }
}
