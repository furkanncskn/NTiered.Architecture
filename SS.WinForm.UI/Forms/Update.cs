using System;
using System.Data;
using System.Windows.Forms;

using Entity.Concrete;
using SS.BusinessLogicLayer.Provider;

namespace SS.WinForm.UI.Forms
{
    public partial class Update : Form
    {
        public Update()
        {
            InitializeComponent();
        }

        #region EventHandlers
        private void Update_Load(object sender, EventArgs e)
        {
            usersBindingSource.DataSource = new Users();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            DataTable table = UserProvider.UserBBL.SelectByIdToTable(Convert.ToInt32(txtId.Text));

            if (!(table.Rows.Count > 0))
            {
                MessageBox.Show(
                    "Kullanıcı Bulunamadı",
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            dataGridView1.DataSource = table;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            #region GET_PARAMS
            Users user = new Users()
            {
                USER_ID = (int)dataGridView1.CurrentRow.Cells["USER_ID"].Value,
                USER_NAME = (string)dataGridView1.CurrentRow.Cells["USER_NAME"].Value,
                USER_PASSWORD = (string)dataGridView1.CurrentRow.Cells["USER_PASSWORD"].Value,
                USER_EMAIL = (string)dataGridView1.CurrentRow.Cells["USER_EMAIL"].Value,
                USER_IS_ACTIVE = (bool)dataGridView1.CurrentRow.Cells["USER_IS_ACTIVE"].Value,
                USER_REGISTER_DATE = (DateTime)dataGridView1.CurrentRow.Cells["USER_REGISTER_DATE"].Value
            };
            #endregion // GET_PARAMS

            #region UPDATE
            user.USER_IS_ACTIVE = true;
            
            bool succcess = UserProvider.UserBBL.Update(user);
            #endregion // UPDATE

            #region ERR_MSG
            if(succcess == false)
            {
                MessageBox.Show("Güncelleme işlemi başarısız oldu!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Güncelleme işlemini başarıyla gerçekleştirdiniz!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            #endregion 
        }
      
        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Columns.Count > 0)
            {
                if (dataGridView1.Columns.Contains("USER_ID") == true) { dataGridView1.Columns["USER_ID"].Visible = false; }

                if (dataGridView1.Columns.Contains("USER_PASSWORD") == true) { dataGridView1.Columns["USER_PASSWORD"].ReadOnly = true; }

                if (dataGridView1.Columns.Contains("USER_REGISTER_DATE") == true) { dataGridView1.Columns["USER_REGISTER_DATE"].ReadOnly = true; }

                if (dataGridView1.Columns.Contains("USER_IS_ACTIVE") == true) { dataGridView1.Columns["USER_IS_ACTIVE"].ReadOnly = true; }
            }
        }
        #endregion // EventHandlers
    }
}
