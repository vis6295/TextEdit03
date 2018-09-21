using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextEdit.Lib;

namespace TextEdit03.App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        TextData textData = new TextData();

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("button1_Click");
            textData.Load("..\\..\\Form1.cs");
            int cnt = textData.Count;
            for (int i = 0; i < cnt; i++) {
                richTextBox1.AppendText("\n" + textData.GetLine(i));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TextView textView = new TextView(panel2, textData);
            MessageBox.Show($"{textView.iH} * {textView.iW}");

            textView.Paint(panel2.CreateGraphics());

        }
    }
}
