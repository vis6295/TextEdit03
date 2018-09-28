using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextEditLib;

//========================================================================================================
//=======================================================================================================
//======================================================================================================
//=====================================================================================================
//====================================================================================================
//===================================================================================================
//==================================================================================================
//=================================================================================================
//================================================================================================
//===============================================================================================
//==============================================================================================
//=============================================================================================
//============================================================================================
//===========================================================================================
//==========================================================================================
//=========================================================================================
//========================================================================================
//=======================================================================================
//======================================================================================
//=====================================================================================
//====================================================================================
//===================================================================================
//==================================================================================
//=================================================================================



namespace TextEdit03.App
{
    public partial class Form1 : Form
    {
        TextEdit textEdit = null;

        public Form1()
        {

            InitializeComponent();

            log.logMsg += s => richTextBox1.AppendText(s+"\n");
            log.logMsg2 += s => textBox1.Text=s;

            TextData textData = new TextData();

            textData.Load("..\\..\\Form1.cs");
            int cnt = textData.Count;
            for (int i = 0; i < cnt; i++)
            {
                richTextBox1.AppendText("\n" + textData.GetLine(i));
            }

            textEdit = new TextEdit(textData);

            this.SuspendLayout();

            this.Controls.Add(textEdit);
            textEdit.Dock = DockStyle.Fill;
            textEdit.Parent = panel2;

            this.ResumeLayout(false);

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{textEdit.CanFocus} {textEdit.CanSelect}");
            bool flag = textEdit.Focus();
            MessageBox.Show($"{flag}");

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            textEdit.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Beep.Play();
            System.Media.SystemSounds.Asterisk.Play();
            System.Media.SystemSounds.Exclamation.Play();
            System.Media.SystemSounds.Question.Play();
            System.Media.SystemSounds.Hand.Play();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Console.Beep();
            Console.Beep(1000, 75);
            //Console.Beep(2000, 100);
            //Console.Beep(500, 100);
        }
    }
}
