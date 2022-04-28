using Entity.Concrete;
using SS.BusinessLogicLayer.Commen;
using SS.DataAccessLayer.Concrete;
using System.Configuration;

namespace SS.BusinessLogicLayer.BBL
{
    public class UsersBBL : BaseBBL<Users>
    {
        public UsersBBL()
        {
            DbProvider.ConnectionString = ConfigurationManager.ConnectionStrings["SpaceSurgeon"].ToString();

            DbProvider.ProviderType = DbProviderType.SqlServer;
        }
    }
}
