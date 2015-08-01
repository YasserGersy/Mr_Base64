using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace B64ToImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void RtxbxString_TextChanged(object sender, EventArgs e)
        {
            Image i = String2Image(RtxbxString.Text);
            if (i != null)
                pictureBoxFrom64.Image = i;
        }
        public Image String2Image(string p)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(p);


                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }

                return image;
            }
            catch { return null; }
        }
        public string Fileto64(string filepath)
        {
            try
            {
                byte[] byts = System.IO.File.ReadAllBytes(filepath);

                return Convert.ToBase64String(byts);
            }
            catch { return null; }
        }

        private void lnklblLoadFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                OpenFileDialog o = new OpenFileDialog();
                if (o.ShowDialog() == DialogResult.OK)
                    RtxbxString.Text = System.IO.File.ReadAllText(o.FileName);
            }
            catch { SetNotf("Error"); }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            if (o.ShowDialog() == DialogResult.OK)
                txbxPathOfFile.Text = o.FileName;
        }

        private void txbxPathOfFile_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(txbxPathOfFile.Text))
            {
                string p = Fileto64(txbxPathOfFile.Text);

                if (p != null)
                {
                    richTextBox1.Text = p;
                    SetNotf("Loaded Successfully");
                    try
                    {
                        pictureBoxTo64.Image = Image.FromFile(txbxPathOfFile.Text);
                    }
                    catch { }
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog();
            if (s.ShowDialog() == DialogResult.OK)
            {
                s.FileName = "Base64_string__.txt";
                File.WriteAllText(s.FileName, richTextBox1.Text);
                SetNotf("Saved Successfully");
            }
        }

        private void SetNotf(string p)
        {
            lblNot.Text = p;
        }

        private void btnSaveImageToFile_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog s = new SaveFileDialog();
                s.FileName = "MyImage__.jpg";

                if (s.ShowDialog() == DialogResult.OK)
                    pictureBoxFrom64.Image.Save(s.FileName);
            }
            catch
            {
                SetNotf("Error Saving");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 4;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeSizeMode();
        }

        private void ChangeSizeMode()
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0: pictureBoxFrom64.SizeMode = PictureBoxSizeMode.Normal; break;
                case 1: pictureBoxFrom64.SizeMode = PictureBoxSizeMode.StretchImage; break;
                case 2: pictureBoxFrom64.SizeMode = PictureBoxSizeMode.AutoSize; break;
                case 3: pictureBoxFrom64.SizeMode = PictureBoxSizeMode.CenterImage; break;
                case 4: pictureBoxFrom64.SizeMode = PictureBoxSizeMode.Zoom; break;
            }
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }


        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private void Encode_str2b64_Click(object sender, EventArgs e)
        {
            richTextBox_Base64.Text = Base64Encode(richTextBox_String_.Text);
        }
         
        private void Decode_Click(object sender, EventArgs e)
        {
            richTextBox_String_.Text = Base64Decode(richTextBox_Base64.Text);

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new FrmAbout().Show();
        }
    }
}
