using System;
using System.Windows.Forms;

using SS.WinForm.UI.Forms;
using SS.BusinessLogicLayer.Provider;

namespace SS.WinForm.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region EventHandler
        private void btnGetAll_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = UserProvider.UserBBL.SelectAll();
        }

        private void btnGetCount_Click(object sender, EventArgs e)
        {
            txtSumQueryCount.Text = UserProvider.UserBBL.GetCount().ToString();
        }
        
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var row = dataGridView1.CurrentRow;

            if (row == null) return;

            var id = row.Cells["USER_ID"].Value;

            if ((int)id < 1) return;

            bool success = UserProvider.UserBBL.DeleteById((int)id);

            if(success == false)
            {
                MessageBox.Show(
                    "Silme işlemi gerçekleştirilemedi",
                    "Message",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );

                return;
            }

            dataGridView1.DataSource = UserProvider.UserBBL.SelectAll();

            MessageBox.Show(
                    "Silme işlemi başarılı",
                    "Message",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
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
