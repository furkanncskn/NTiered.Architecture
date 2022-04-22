using Entity.Concrete;
using SS.DataAccessLayer.Abstract;
using SS.WinForm.UI.Common;
using SS.WinForm.UI.Forms;
using System;
using System.Data;
using System.Windows.Forms;

namespace SS.WinForm.UI
{
    public partial class MainForm : Form
    {
        private static IDataAccess<Users> DB = DBProvider<Users>.DB;

        public MainForm()
        {
            InitializeComponent();
        }

        #region EventHandler
        private void btnGetAll_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DB.TableFromQuery(
                DBProvider<Users>.connectionString, 
                "select * from Users", 
                CommandType.Text);
        }

        private void btnGetCount_Click(object sender, EventArgs e)
        {
            txtSumQueryCount.Text = ((int)DB.ToScalerValue(
                DBProvider<Users>.connectionString, 
                "select count(*) from Users", 
                CommandType.Text)).ToString();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            ListForm form = new ListForm();

            form.ShowDialog();
        }
        #endregion 
    }
}
