using Entity.Concrete;
using Entity.Validations;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using SS.BusinessLogicLayer.BBL;
using SS.BusinessLogicLayer.Commen;
using SS.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SS.WinForm.UI.Forms
{
    public partial class Update : Form
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
            DataTable table = UserBBL.SelectByIdToTable(Convert.ToInt32(txtId.Text));

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
                USER_EMAIL = (string)dataGridView1.CurrentRow.Cells["USER_EMAIL"].Value
            };
            #endregion // GET_PARAMS

            #region UPDATE
            SqlCommand command = DBProvider.DB.CreateCommand(CommandType.StoredProcedure, "sp_update_User");

            int result = 0;
            
            command.AddSqlParameter("USER_ID", ParameterDirection.Input, SqlDbType.Int, user.USER_ID);
            command.AddSqlParameter("USER_NAME", ParameterDirection.Input, SqlDbType.VarChar, user.USER_NAME);
            command.AddSqlParameter("USER_PASSWORD", ParameterDirection.Input, SqlDbType.VarChar, user.USER_PASSWORD);
            command.AddSqlParameter("USER_EMAIL", ParameterDirection.Input, SqlDbType.VarChar, user.USER_EMAIL);
            command.AddSqlParameter("RETURN_VALUE", ParameterDirection.ReturnValue, SqlDbType.Int, result);

            DBProvider.DB.NonQueryCommand(command, DBProvider.connectionString);
            #endregion // UPDATE

            #region ERR_MSG
            int returnValue = (int)command.Parameters["RETURN_VALUE"].Value;
            
            if(returnValue == 0)
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
