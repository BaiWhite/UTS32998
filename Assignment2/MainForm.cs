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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Assignment2
{
    public partial class MainForm : Form
    {
        static AccountList AccountList = new AccountList();

        public MainForm()
        {
            InitializeComponent();

            bool more = true;
            while (more)
            {
                TextEditorForm textEditor = new TextEditorForm();
                textEditor.ShowDialog();
            }
        }
    }
}
