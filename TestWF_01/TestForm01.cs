using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestWF_01
{
    public partial class TestForm01 : Form
    {
        Control2 control;

        public TestForm01()
        {
            InitializeComponent();

            log.onMsg += (a) => sb.Append("\n"+a);
            timer1.Enabled = true;

            control = new Control2();
            this.Controls.Add(control);
            control.BackColor = Color.Green;
            control.ForeColor = Color.Red;
            control.Dock = DockStyle.Fill;
            control.Parent = panel2;
        }

        StringBuilder sb = new StringBuilder();

        private void timer1_Tick(object sender, EventArgs e)
        {
            richTextBox1.AppendText(sb.ToString());
            sb.Clear();
        }

        private void panel2_Resize(object sender, EventArgs e)
        {
            //sb.Append("panel2_Resize" + "\n");
        }

        private void panel2_SizeChanged(object sender, EventArgs e)
        {
            //sb.Append("panel2_SizeChanged" + "\n");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }
    }

    delegate void OnMsg(string str);

    static class log {
        public static event OnMsg onMsg;
        public static void msg(string str) {
            OnMsg tmp = onMsg;
            if (tmp != null) tmp(str);
        }
    }

}
