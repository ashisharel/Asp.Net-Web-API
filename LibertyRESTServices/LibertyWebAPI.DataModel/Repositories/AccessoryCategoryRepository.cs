using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DataModel.Contracts;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace LibertyWebAPI.DataModel.Repositories
{
    public class AccessoryCategoryRepository : BaseRepository<AccessoryCategory>, IAccessoryCategoryRepository
    {
        private AccessoryCategory root;
        private IList<AccessoryLevel> level1Items;
        private IList<AccessoryLevel> level2Items;
        private static string StoredProcedureName = "p_PEP_Logo";

        public AccessoryCategory GetProductAccents(string productId, string categoryId, int sessionId)
        {
            level1Items = new List<AccessoryLevel>();
            level2Items = new List<AccessoryLevel>();
            root = new AccessoryCategory() { Name = "Accent Symbols", Code = "accent" };
            SqlCommand cmd = new SqlCommand(StoredProcedureName);
            cmd.Parameters.AddWithValue("@Session_ID", sessionId);
            cmd.Parameters.AddWithValue("@Product", productId);
            cmd.Parameters.AddWithValue("@ImageTypes", "A"); // [A]ccents = Corner
            base.ExecuteStoredProc(cmd);
            var list = root.Groups.OrderBy(g => g.Name).ToList();
            root.Groups = list;
            return root;
        }

        public AccessoryCategory GetProductBackgrounds(string productId, string categoryId, int sessionId)
        {
            level1Items = new List<AccessoryLevel>();
            level2Items = new List<AccessoryLevel>();
            root = new AccessoryCategory() { Name = "Background Symbols", Code = "background" };
            SqlCommand cmd = new SqlCommand(StoredProcedureName);
            cmd.Parameters.AddWithValue("@Session_ID", sessionId);
            cmd.Parameters.AddWithValue("@Product", productId);
            cmd.Parameters.AddWithValue("@ImageTypes", "P"); // [P]hantom = Center = Background
            base.ExecuteStoredProc(cmd);
            var list = root.Groups.OrderBy(g => g.Name).ToList();
            root.Groups = list;
            return root;
        }

        public AccessoryCategory GetProductOneliners(string productId, int sessionId)
        {
            level1Items = new List<AccessoryLevel>();
            level2Items = new List<AccessoryLevel>();
            root = new AccessoryCategory() { Name = "One-Liners", Code = "root" };
            SqlCommand cmd = new SqlCommand(StoredProcedureName);
            cmd.Parameters.AddWithValue("@Session_ID", sessionId);
            cmd.Parameters.AddWithValue("@Product", productId);
            cmd.Parameters.AddWithValue("@ImageTypes", "S"); // SigCut = One-liners
            base.ExecuteStoredProc(cmd);
            var list = root.Groups.OrderBy(g => g.Name).ToList();
            root.Groups = list;
            return root;
        }

        public override AccessoryCategory PopulateRecord(IDataReader reader, int resultCount)
        {
            if (resultCount == 1)
            {
                level1Items.Add(
                    new AccessoryLevel()
                    {
                        ParentId = null,
                        CodeId = reader["logo_web_category_uid"].ToString(),
                        Name = reader["category_description"].ToString(),
                    });
                return null;
            }
            if (resultCount == 2)
            {
                level2Items.Add(
                    new AccessoryLevel()
                    {
                        ParentId = reader["parent_logo_web_category_uid"].ToString(),
                        CodeId = reader["logo_web_category_uid"].ToString(),
                        Name = reader["category_description"].ToString(),
                    });
                return null;
            }
            if (resultCount == 3)
            {
                var parentId = reader["parent_logo_web_category_uid"].ToString();
                var code3Id = reader["logo_web_category_uid"].ToString();
                var code3Name = reader["category_description"].ToString();
                var parent = level2Items.FirstOrDefault(l => l.CodeId == parentId);

                // find the first level
                var level1 = root.Groups.FirstOrDefault(g => g.Code == parent.ParentId);
                if (level1 == null)
                {
                    var item = level1Items.FirstOrDefault(i => i.CodeId == parent.ParentId);
                    level1 = new AccessoryCategory()
                    {
                        Name = item.Name,
                        Code = item.CodeId,
                    };
                    root.Groups.Add(level1);
                }

                // find the second level in the first level
                var level2 = level1.Groups.FirstOrDefault(i => i.Code == parent.CodeId);
                if (level2 == null)
                {
                    level2 = new AccessoryCategory()
                    {
                        Name = parent.Name,
                        Code = parent.CodeId,
                    };
                    level1.Groups.Add(level2);
                }

                // find the third level in the second level
                var level3 = level2.Groups.FirstOrDefault(i => i.Code == code3Id);
                if (level3 == null)
                {
                    level3 = new AccessoryCategory()
                    {
                        Name = code3Name,
                        Code = code3Id,
                    };
                    level2.Groups.Add(level3);
                }

                level3.Items.Add(new AccessoryDetails()
                {
                    Code = reader["Logo_ID"].ToString(),
                    Name = reader["Logo_Name"].ToString(),
                    Url = reader["LogoUrl"].ToString(),
                    Pricing = null,
                    Type = reader["ImgType"].ToString(),
                });
                return null;
            }
            return null;
        }
    }

    internal class AccessoryLevel
    {
        public string ParentId { get; set; }
        public string CodeId { get; set; }
        public string Name { get; set; }
    }
}