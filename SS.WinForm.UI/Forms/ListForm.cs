using Entity.Concrete;
using SS.BusinessLogicLayer.BBL;
using SS.BusinessLogicLayer.Commen;
using SS.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SS.WinForm.UI.Forms
{
    public partial class ListForm : Form
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

        public ListForm()
        {
            InitializeComponent();
        }

        private void AddListToListBox(List<Users> list)
        {
            listBox1.Items.Add("KAYITLAR");
            int count = 1;
            foreach (Users item in list)
            {
                listBox1.Items.Add("");
                listBox1.Items.Add("[ " + count.ToString() + " ].Kayıt");
                listBox1.Items.Add("******************************************************");
                listBox1.Items.Add("KULLANICI ADI: " + item.USER_NAME);
                listBox1.Items.Add("E-POSTA: " + item.USER_EMAIL);
                listBox1.Items.Add("KAYIT TARİHİ: " + item.USER_REGISTER_DATE);
                listBox1.Items.Add("******************************************************");
                count++;
            }
        }

        #region EventHandler
        private void ListForm_Load(object sender, EventArgs e)
        {
            List<Users> users = UserBBL.SelectAll();

            AddListToListBox(users);
        }
        #endregion
    }
}
