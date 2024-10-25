using System.Data;

namespace Karyon.Models
{
    public class CategoriesRequest : Menu
    {
        public long? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? ImageUrl { get; set; }
        //public string? AchieveImageUrl { get; set; }
        public int? CategoryCount { get; set; }

        public DataSet GetAllCategories()
        {
            try
            {               
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetAllCategories);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class Categories
    {
        public List<CategoriesRequest>? CategoryList { get; set; }     
    }
    public class CategoryReposne
    {
       public Categories? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
}
