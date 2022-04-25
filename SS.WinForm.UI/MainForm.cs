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
            dataGridView1.DataSource = DBProvider.DB.TableFromQuery(
                                            DBProvider.connectionString,
                                                "select * from Users",
                                                    CommandType.Text
                                        );
        }

        private void btnGetCount_Click(object sender, EventArgs e)
        {
            txtSumQueryCount.Text = ((int)DBProvider.DB.ToScalerValue (
                                        DBProvider.connectionString,
                                            "select count(*) from Users",
                                                CommandType.Text
                                        )
                                    ).ToString();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            ListForm form = new ListForm();

            if (form != null) { form.ShowDialog(); }
        }
        
        private void btnRegister_Click(object sender, EventArgs e)
        {
            Add form = new Add();

            if (form != null) { form.ShowDialog(); }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            Update form = new Update();

            if (form != null) { form.ShowDialog(); }
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Columns.Count > 0)
            {
                if (dataGridView1.Columns.Contains("USER_ID") == true)
                {
                    dataGridView1.Columns["USER_ID"].Visible = false;
                }
            }
        }
        #endregion
    }
}
