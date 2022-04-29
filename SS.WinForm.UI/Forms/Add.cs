using System;
using System.Windows.Forms;

using Entity.Concrete;
using Entity.Validations;
using FluentValidation.Results;
using SS.BusinessLogicLayer.Provider;

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
            Users user = new Users()
            {
                USER_NAME = txtName.Text,
                USER_PASSWORD = txtPassword.Text,
                USER_EMAIL = txtEmail.Text,
                USER_REGISTER_DATE = DateTime.Now,
                USER_IS_ACTIVE = true
            };

            ValidationResult result = new UserValidator().Validate(user);

            if (!result.IsValid)
            {
                string errorMessage = UserValidator.GetErrorMessage(result);

                MessageBox.Show(
                        errorMessage,
                        "Error Message",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                return;
            }
            #endregion // VALIDATION

            #region ADD
            bool success = UserProvider.UserBBL.Insert(user);
            #endregion // ADD

            #region ERR_MSG
            if (success == false)
            {
                MessageBox.Show(
                    "Kayıt işlemi başarısız oldu",
                    "Result",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                MessageBox.Show(
                    "Tebrikler, kayıt başarı ile gerçekleşti.\nAilemize Hoşgeldiniz :)", 
                    "Result", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information
                );
            }
            #endregion //ERR_MSG
        }

        private void Add_Load(object sender, EventArgs e)
        {
            usersBindingSource.DataSource = new Users();
        }
        #endregion // EventHandlers
    }
}
