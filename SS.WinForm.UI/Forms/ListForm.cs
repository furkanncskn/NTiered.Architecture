using Entity.Concrete;
using SS.DataAccessLayer.Abstract;
using SS.WinForm.UI.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SS.WinForm.UI.Forms
{
    public partial class ListForm : Form
    {
        private static IDataAccess<Users> DB = DBProvider<Users>.DB;

        public ListForm()
        {
            InitializeComponent();
        }

        #region EventHandler
        private void ListForm_Load(object sender, EventArgs e)
        {
            List<Users> list = DB.ListFromQuery(
                DBProvider<Users>.connectionString, 
                "select * from Users", 
                CommandType.Text
                );

            Users user = new Users();

            listBox1.Items.Add("KAYITLAR");
            foreach (Users item in list)
            {
                listBox1.Items.Add("");
                listBox1.Items.Add("******************************************************");
                listBox1.Items.Add("KULLANICI ADI: " + item.USER_NAME);
                listBox1.Items.Add("E-POSTA: " + item.USER_EMAIL);
                listBox1.Items.Add("KAYIT TARİHİ:" + item.USER_REGISTER_DATE);
                listBox1.Items.Add("******************************************************");
            }
        }
        #endregion
    }
}
