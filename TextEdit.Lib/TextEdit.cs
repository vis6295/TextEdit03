using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEdit.Lib
{
    public class TextEdit: Control
    {
        //25.09.2018.1
        Bitmap bitmap = null;
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            e.Graphics.DrawImage(bitmap, new Point(0, 0));
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            //base.OnSizeChanged(e);
            Bitmap bm = new Bitmap(Width, Height);
            Graphics gr = Graphics.FromImage(bm);
            gr.Clear(Color.Black);
            TextRenderer.DrawText(gr, "Text edit", new Font("Times New Roman", 24), new Point(0, 0), Color.Green);
            bitmap = bm;
        }

        //25.09.2018.2

        public TextView textView;
        public TextData textData;

        public TextEdit() {
            textView = new TextView(this);
        }
    }
}
