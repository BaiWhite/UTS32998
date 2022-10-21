using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextEditor;

namespace Assignment2
{
    public partial class NewUserForm : Form
    {
        static AccountList AccountList = new AccountList();
        public NewUserForm()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (PasswordBox.Text == RePasswordBox.Text)
            {
                CreateUser();
            }
            DialogResult = DialogResult.OK;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void CreateUser()
        {
            User user = new User
            {
                Name = UsernameBox.Text,
                Password = PasswordBox.Text,
                User_Type = TypeComboBox.Text,
                First_Name = FirstNameBox.Text,
                Last_Name = LastNameBox.Text,
                Birth_Date = BirthPicker.Value.ToString("dd-MMM-yyyy"),
            };

            AccountList.Add(user);
            AccountList.Close();

            Close();
        }
    }
}
