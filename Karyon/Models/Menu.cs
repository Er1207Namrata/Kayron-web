using Karyon.Models;
using System.Data.SqlClient;
using System.Data;
using System.Reflection.Emit;

namespace Karyon.Models
{
    public class Menu : Common
    {
        public string? MenuId { get; set; }
        public string? MenuName { get; set; }
        public string? SubMenuName { get; set; }
        public int? PermissionId { get; set; }
        public string? Fk_EmpId { get; set; }
        public string? Fk_MenuId { get; set; }
        public int? SubMenuId { get; set; }
        public string? Url { get; set; }
        public string? Class { get; set; }
        public string? SubMenuIcon { get; set; }
        public string? Icon { get; set; }
        public string? IsDropdown { get; set; }
        public string? LoginId { get; set; }
        public string? Password { get; set; }
        public string? MobileIcon { get; set; }
        public List<Menu>? menuList { get; set; }
        public List<Menu>? subMenuList { get; set; }

        public DataSet GetMenuDetails()
        {
            try
            {
                SqlParameter[] para =
                {
                  new SqlParameter("@Fk_MemId", Fk_MemId),
                  new SqlParameter("@OpCode", OpCode),
                  new SqlParameter("@Fk_FranchiseId", Fk_FranchiseId),

                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.BindMenuMaster, para);
                return ds;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    public class MenuResponse
    {
        public int? Status { get; set; }
        public string? Message { get; set; }
        public MenuResponseList? Response { get; set; }
    }
    public class MenuResponseList
    {
        public MenuDataList? MenuList { get; set; }

        public List<MenuData>? Menu { get; set; }
    }

    public class MenuDataList
    {
        public List<MenuData>? Menu { get; set; }
    }

    

    public class MenuData
    {
        public string? MenuName { get; set; }
        public string? Icon { get; set; }
        public List<SubMenuData>? SubMenu { get; set; }

    }

    public class SubMenuData
    {
        public string? SubMenuName { get; set; }
    }


    public class MenuDataResp:Menu
    {
        public string? Message { get; set; }
        public MenuList? responselist { get; set; }

    }
    public class MenuList 
    {
        public List<Menu>? menuList { get; set; }

        public List<Menu>? subMenuList { get; set; }
    }
}    
     