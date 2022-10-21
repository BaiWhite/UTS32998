using Assignment2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TextEditor
{
    public partial class TextEditorForm : Form
    {
        static AccountList AccountList = new AccountList();
        static int TabCount = 0;
        private RichTextBox GetCurrentDoc
        {
            get { return (RichTextBox)EditorTabControl.SelectedTab.Controls["Body"]; }
        }

        public TextEditorForm()
        {
            InitializeComponent();

            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            if (loginForm.DialogResult != DialogResult.OK)
            {
                AccountList.Close();

                Environment.Exit(0);
            }
            else
            {
                UserToolStripLabel.Text = "Hello, " + AccountList.Login;
                Add();
                PopulateFontSizes();
            }
        }

        #region Menu
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void PasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Past();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About();
        }
        #endregion

        #region StripButton
        private void NewToolStripButton_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void OpenToolStripButton_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void SaveAsToolStripButton_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void CutToolStripButton_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void CopyToolStripButton_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void PasterToolStripButton_Click(object sender, EventArgs e)
        {
            Past();
        }

        private void HelpToolStripButton_Click(object sender, EventArgs e)
        {
            About();
        }

        private void BoldToolStripButton_Click(object sender, EventArgs e)
        {
            Font change = new Font(GetCurrentDoc.SelectionFont, GetCurrentDoc.SelectionFont.Style ^ FontStyle.Bold);

            GetCurrentDoc.SelectionFont = change;
        }

        private void ItalicsToolStripButton_Click(object sender, EventArgs e)
        {
            Font change = new Font(GetCurrentDoc.SelectionFont, GetCurrentDoc.SelectionFont.Style ^ FontStyle.Italic);

            GetCurrentDoc.SelectionFont = change;
        }

        private void UnderlineToolStripButton_Click(object sender, EventArgs e)
        {
            Font change = new Font(GetCurrentDoc.SelectionFont, GetCurrentDoc.SelectionFont.Style ^ FontStyle.Underline);

            GetCurrentDoc.SelectionFont = change;
        }

        private void SizeToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            float NewSize;

            float.TryParse(SizeToolStripComboBox.SelectedItem.ToString(), out NewSize);

            Font NewFont = new Font(GetCurrentDoc.SelectionFont.Name, NewSize, GetCurrentDoc.SelectionFont.Style);

            GetCurrentDoc.SelectionFont = NewFont;
        }
        #endregion

        #region Function
        private void Add()
        {
            RichTextBox textBox = new RichTextBox();

            textBox.Name = "Body";
            textBox.Dock = DockStyle.Fill;
            textBox.ContextMenuStrip = ContextMenuStrip;

            TabPage newPage = new TabPage();
            TabCount += 1;

            string docText = "Doc " + TabCount;
            newPage.Name = docText;
            newPage.Text = docText;
            newPage.Controls.Add(textBox);

            EditorTabControl.TabPages.Add(newPage);
            EditorTabControl.SelectedTab = newPage;
        }

        private void Open()
        {
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = "RTF|*.rtf|Text Files|*.txt|VB Files|*.vb|C# Files|*.cs|All Files|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string ext = Path.GetExtension(openFileDialog.FileName);
                switch (ext)
                {
                    case ".rtf":
                        if (openFileDialog.FileName.Length > 9)
                        {
                            GetCurrentDoc.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.RichText);
                        }
                        break;
                    case ".txt":
                        string[] lines = File.ReadAllLines(openFileDialog.FileName);

                        GetCurrentDoc.Clear();
                        foreach (string line in lines)
                        {
                            GetCurrentDoc.AppendText(line + Environment.NewLine);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void Save()
        {
            saveFileDialog.FileName = EditorTabControl.SelectedTab.Name;
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "RTF|.rtf";
            saveFileDialog.Title = "Save";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog.FileName.Length > 0)
                {
                    GetCurrentDoc.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
                }
            }
        }

        private void SaveAs()
        {
            saveFileDialog.FileName = EditorTabControl.SelectedTab.Name;
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "Text Files|*.txt|VB Files|*.vb|C# Files|*.cs|All Files|*.*";
            saveFileDialog.Title = "Save As";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog.FileName.Length > 0)
                {
                    GetCurrentDoc.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                }
            }
        }
        
        private void Cut()
        {
            GetCurrentDoc.Cut();
        }

        private void Copy()
        {
            GetCurrentDoc.Copy();
        }

        private void Past()
        {
            GetCurrentDoc.Paste();
        }

        private void About()
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void PopulateFontSizes()
        {
            for (int i = 8; i <= 20; i++)
            {
                SizeToolStripComboBox.Items.Add(i);
            }

            SizeToolStripComboBox.SelectedIndex = 11;
        }

        private void deleteTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EditorTabControl.TabPages.Count != 1)
            {
                EditorTabControl.TabPages.Remove(EditorTabControl.SelectedTab);
            }
            else
            {
                EditorTabControl.TabPages.Remove(EditorTabControl.SelectedTab);
                Add();
            }
        }
        #endregion
    }
}
