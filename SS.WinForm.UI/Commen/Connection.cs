using Entity.Concrete;
using SS.BusinessLogicLayer.BBL;
using SS.BusinessLogicLayer.Commen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS.WinForm.UI.Commen
{
    public static class Connection
    {
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
