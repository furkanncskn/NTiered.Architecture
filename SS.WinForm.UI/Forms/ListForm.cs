using System;
using System.Windows.Forms;
using System.Collections.Generic;

using Entity.Concrete;
using SS.BusinessLogicLayer.Provider;

namespace SS.WinForm.UI.Forms
{
    public partial class ListForm : Form
    {
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
            List<Users> users = UserProvider.UserBBL.SelectAll();

            AddListToListBox(users);
        }
        #endregion
    }
}
