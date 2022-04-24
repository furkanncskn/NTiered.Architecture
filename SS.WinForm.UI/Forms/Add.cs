using Entity.Concrete;
using SS.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SS.WinForm.UI.Forms
{
    public partial class Add : Form
    {
        public Add()
        {
            InitializeComponent();
        }

        #region EventHandlers
        private void btnAdd_Click(object sender, EventArgs e)
        {
            #region VALIDATION
            usersBindingSource.EndEdit();

            Users user = usersBindingSource.DataSource as Users;
            
            ValidationContext context = new ValidationContext(user);
            
            ICollection<ValidationResult> results = new List<ValidationResult>();
            
            if(!Validator.TryValidateObject(user,context,results))
            {
                foreach (var item in results)
                {
                    MessageBox.Show(
                            "Error Message: " + item.ErrorMessage, 
                            "Message", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Warning
                        );
                }

                return;
            };
            #endregion // VALIDATION

            #region ADD
            SqlCommand command = new SqlCommand()
            {
                CommandText = "sp_insert_Users",
                CommandType = CommandType.StoredProcedure
            };

            command.AddSqlParameter("USER_NAME", ParameterDirection.Input, SqlDbType.VarChar, txtName.Text ,150);
            command.AddSqlParameter("USER_PASSWORD", ParameterDirection.Input, SqlDbType.VarChar, txtPassword.Text, 150);
            command.AddSqlParameter("USER_EMAIL", ParameterDirection.Input, SqlDbType.VarChar, txtEmail.Text, 150);

            int affectedRow = DBProvider.DB.NonQueryCommand(command, DBProvider.connectionString);
            #endregion // ADD

            #region ERR_MSG
            if (!(affectedRow > 0))
                MessageBox.Show("Kayıt işlemi başarısız oldu", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Tebrikler, kayıt başarı ile gerçekleşti.\nAilemize Hoşgeldiniz :)", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            #endregion //ERR_MSG
        }

        private void Add_Load(object sender, EventArgs e)
        {
            usersBindingSource.DataSource = new Users();
        }
        #endregion // EventHandlers
    }
}
