using Entity.Concrete;
using SS.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

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
            #region VALIDATION
            usersBindingSource.EndEdit();
            
            Users user = usersBindingSource.Current as Users;
            
            PropertyInfo property = typeof(Users).GetProperties().Where(p => p.Name == "USER_NAME").First();

            ValidationContext context = new ValidationContext(user)
            {
                MemberName = property.Name,
            };

            ICollection<ValidationResult> results = new List<ValidationResult>();
            
            if(!Validator.TryValidateProperty(user.USER_NAME, context, results))
            {
                foreach (var item in results)
                {
                    MessageBox.Show(
                        "Hata Mesajı: " + item.ErrorMessage,
                        "Message",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
                }
            }
            #endregion //VALIDATION

            #region GET
            SqlCommand command = new SqlCommand()
            {
                CommandText = "sp_get_user_by_name",
                CommandType = CommandType.StoredProcedure,
            };

            command.AddSqlParameter("USER_NAME", ParameterDirection.Input, SqlDbType.Text, txtName.Text);

            DataTable table = DBProvider.DB.TableFromQuery (command, DBProvider.connectionString);

            if (table.Rows.Count > 0)
            {
                dataGridView1.DataSource = table;
                
                return;
            }
            #endregion // GET

            #region ERR_MSG
            MessageBox.Show(
                    "Kullanıcı Bulunamadı",
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
            #endregion // ERR_MSG
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
            SqlCommand command = new SqlCommand()
            {
                CommandText = "sp_update_User",
                CommandType = CommandType.StoredProcedure
            };

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
            dataGridView1.Columns["USER_ID"].Visible = false;
            dataGridView1.Columns["USER_PASSWORD"].ReadOnly = true;
            dataGridView1.Columns["USER_REGISTER_DATE"].ReadOnly = true;
            dataGridView1.Columns["USER_IS_ACTIVE"].ReadOnly = true;
        }
        #endregion // EventHandlers
    }
}
