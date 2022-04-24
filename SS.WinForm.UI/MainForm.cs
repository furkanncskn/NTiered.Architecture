using SS.DataAccessLayer.Concrete;
using SS.WinForm.UI.Forms;
using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace SS.WinForm.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            DBProvider.connectionString = ConfigurationManager.ConnectionStrings["SpaceSurgeon"].ToString();

            InitializeComponent();
        }

        #region EventHandler
        private void btnGetAll_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = DBProvider.DB.TableFromQuery(
                DBProvider.connectionString,
                "select * from Users",
                CommandType.Text);

            dataGridView1.Columns["USER_ID"].Visible = false;
        }

        private void btnGetCount_Click(object sender, EventArgs e)
        {
            txtSumQueryCount.Text = ((int)DBProvider.DB.ToScalerValue (
                DBProvider.connectionString,
                "select count(*) from Users",
                CommandType.Text)).ToString();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            ListForm form = new ListForm();

            form.ShowDialog();
        }
        #endregion

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Add add = new Add();

            add.ShowDialog();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            Update update = new Update();

            update.ShowDialog();
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            dataGridView1.Columns["USER_ID"].Visible = false;
        }
    }
}
