using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Assignment2
{
    public partial class LoginForm : Form
    {
        static AccountList AccountList = new AccountList();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (AccountList.Valid(Username.Text, Password.Text))
                {
                    System.Threading.Thread.Sleep(1000);
                    DialogResult = DialogResult.OK;
                    AccountList.Login = Username.Text;
                    Close();
                }
                else
                {
                    MessageBox.Show("Invalid credentials! Please try again.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid credentials! Please try again.");
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void NewUserButton_Click(object sender, EventArgs e)
        {
            Hide();
            NewUserForm newUser = new NewUserForm();
            newUser.ShowDialog();
            bool a = true;
            while (a)
            {
                if (newUser.DialogResult == DialogResult.OK)
                {
                    Show();
                    a = false;
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
