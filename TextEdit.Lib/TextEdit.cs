using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditLib
{
    public class TextEdit: Control
    {
        //25.09.2018.1
        Bitmap bitmap = null;
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, new Point(0, 0));
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            bitmap = new Bitmap(Width, Height);
            //TextRenderer.DrawText(gr, "Text edit", new Font("Times New Roman", 24), new Point(0, 0), Color.Green);
            textView.Resize();
            textView.Paint(Graphics.FromImage(bitmap));
            this.CreateGraphics().DrawImage(bitmap, new Point(0, 0));
        }

        //25.09.2018.2

        public TextView textView;
        public TextData textData;

        public TextEdit() {
            textView = new TextView(this);
        }

        public TextEdit(TextData textData)
        {
            this.textData = textData;
            textView = new TextView(this);
        }

        public void DrawText()
        {
            textView.Paint(Graphics.FromImage(bitmap));
        }
    }
}
