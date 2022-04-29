using Entity.Concrete;
using System.Configuration;

using SS.BusinessLogicLayer.BBL;
using SS.DataAccessLayer.Concrete;
using SS.BusinessLogicLayer.Commen;

namespace SS.BusinessLogicLayer.Provider
{
    public static class UserProvider
    {
        static UserProvider()
        {
            DbProvider.ConnectionString = ConfigurationManager.ConnectionStrings["SpaceSurgeon"].ToString();

            DbProvider.ProviderType = DbProviderType.SqlServer;
        }

        private static IBBL<Users> _UserBBL;

        public static IBBL<Users> UserBBL
        {
            get
            {
                if (_UserBBL == null)
                {
                    _UserBBL = new UsersBBL();
                }

                return _UserBBL;
            }
        }
    }
}
