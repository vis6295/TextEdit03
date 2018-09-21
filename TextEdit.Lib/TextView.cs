using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEdit.Lib
{
    public class TextView
    {
        public int iTop=0;//верхняя строка
        public int iLeft=0;//левый символ
        public Font font = new Font("Consolas", 10);
        public int fontH, fontW;

        public int iH, iW;
        public Control owner;
        public TextData textData;

        public string[] buf;

        int lp;
        int fontW2;

        public TextView(Control owner, TextData textData) {
            fontH = (int)Math.Ceiling(font.GetHeight());
            fontW = (int)Math.Ceiling(font.SizeInPoints);

            this.owner = owner;
            iH = this.owner.Height / fontH;

            Graphics gr = this.owner.CreateGraphics();
            SizeF pt1 = gr.MeasureString(".", font);
            SizeF pt2 = gr.MeasureString("..", font);

            fontW2 = (int)(pt2.Width - pt1.Width);
            iW = (this.owner.Width-lp) / fontW2;

            lp = (int)(pt1.Width - fontW2);

            this.textData = textData;

            buf = new string[iH];
            for (int i = 0; i < iH; i++) {
                string s = this.textData.GetLine(i);
                if (s == null)
                {
                    s = new string('.', iW);
                }
                else {
                    int len = s.Length;
                    if (len < iW) s = s + new string('.', iW - len);
                    else s = s.Substring(0, iW);
                }
                buf[i] = s;
            }
        }

        public void Paint(Graphics gr) {
            gr.Clear(Color.Black);
            for (int i = 0; i < iH; i++)
            {
                gr.DrawString(buf[i], font, Brushes.White, 0, i*fontH);
            }
            gr.DrawRectangle(Pens.Green, lp, 0, (iW-1) * fontW2, (iH-1) * fontH);

        }
    }
}
