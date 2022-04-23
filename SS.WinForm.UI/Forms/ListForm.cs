using Entity.Concrete;
using SS.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SS.WinForm.UI.Forms
{
    public partial class ListForm : Form
    {
        public ListForm()
        {
            InitializeComponent();
        }

        #region EventHandler
        private void ListForm_Load(object sender, EventArgs e)
        {
            List<Users> list = DataConvert<Users>.ToListFromDataTable(
                                                DBProvider.DB.TableFromQuery(
                                                DBProvider.connectionString,
                                                "select * from Users",
                                                CommandType.Text
                                            )
                                        );

            Users user = new Users();

            listBox1.Items.Add("KAYITLAR");
            int count = 1;
            foreach (Users item in list)
            {
                listBox1.Items.Add("");
                listBox1.Items.Add("[ " + count.ToString() + " ].Kayıt");
                listBox1.Items.Add("******************************************************");
                listBox1.Items.Add("KULLANICI ADI: " + item.USER_NAME);
                listBox1.Items.Add("E-POSTA: " + item.USER_EMAIL);
                listBox1.Items.Add("KAYIT TARİHİ:" + item.USER_REGISTER_DATE);
                listBox1.Items.Add("******************************************************");
                count++;
            }
        }
        #endregion
    }
}
