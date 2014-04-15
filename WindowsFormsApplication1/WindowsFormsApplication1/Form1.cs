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
using System.Xml.Linq;

namespace WindowsFormsApplication1
{
    public partial class SelectDir : Form
    {
        public SelectDir()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void chooseDirBtn_Click(object sender, EventArgs e)
        {
            string folderPath = "";
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowserDialog.SelectedPath;
                dirPathTextBox.Text = folderPath;

            }
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            //string currentDirectory = Directory.GetCurrentDirectory();
            String localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string fileName = "directory.txt";
            string filePath = System.IO.Path.Combine(localAppDataPath, fileName);
            if (Directory.Exists(dirPathTextBox.Text) && !dirPathTextBox.Text.Equals(""))
            {
                System.IO.File.WriteAllText(filePath, dirPathTextBox.Text);
                Application.Exit();
               
               

            }
            else 
            { 
                string message = dirPathTextBox.Text + " is not a valid directory.";
                string caption = "Invalid directory";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
           
            
        }

        

    }
}
