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
        public Font font = new Font("Consolas", 10);

        public int iTop=0;//верхняя строка
        public int iLeft=0;//левый символ

        public float fontH, fontW, extW, extH;

        public int iH, iW;

        public Control owner;
        public TextData textData;

        public string[] buf;


        public TextView(Control owner, TextData textData) {

            Graphics gr = Graphics.FromHwnd(IntPtr.Zero);
            SizeF pt1 = gr.MeasureString("*", font);
            SizeF pt2 = gr.MeasureString("**\n**", font);

            fontW = pt2.Width - pt1.Width;
            extW = pt1.Width - fontW;

            fontH = pt2.Height - pt1.Height;
            extH = pt1.Height - fontH;

            //fontH = font.GetHeight();
            //fontW = (int)Math.Ceiling(font.SizeInPoints);

            this.owner = owner;
            iH = (int)(this.owner.Height / fontH);
            iW = (int)((this.owner.Width - extW) / fontW);

            this.textData = textData;
            buf = new string[iH];
            for (int i = 0; i < iH; i++) buf[i] = this.textData.GetLine(i);
        }

        void DrawLine(Graphics gr, int iy) //int ix,
        {
            int t = 1;

            string s = buf[iy];
            if (s == null || s == "")
            {
                s = new string('.', iW - t) + "1";
            }
            else
            {
                int len = s.Length;
                if (len == iW - t) s = s + "4";
                else if (len < iW - t) s = s + new string('.', iW - t - len) + "2";
                else s = s.Substring(0, iW - t) + "3";
            }

            gr.DrawString(s, font, Brushes.White, 0, iy * fontH);
        }

        void DrawLine2(Graphics gr, int ix, int iy) //int ix,
        {
            int t = 1;

            string s = buf[iy];
            if (s == null || s == "")
            {
                s = new string('.', iW - t) + "1";
            }
            else
            {
                int len = s.Length;
                if (len == iW - t) s = s + "4";
                else if (len < iW - t) s = s + new string('.', iW - t - len) + "2";
                else s = s.Substring(0, iW - t) + "3";
            }

            gr.DrawString(s, font, Brushes.White, 0, iy * fontH);
        }


        public void Paint(Graphics gr) {
            gr.Clear(Color.Black);

            for (int i = 0; i < iH; i++) DrawLine(gr, i);

            gr.DrawRectangle(Pens.Red, fontW+extW/2, fontH + extH / 2, (iW-1) * fontW, (iH-1) * fontH);

            for (int i = 0; i < iW; i++) {
                float x = fontW * i + extW / 2;
                gr.DrawLine(Pens.Green, x, 0, x, (iH - 1) * fontH);
            }
            for (int i = 0; i < iH; i++)
            {
                float y = fontH * i + extH / 2;
                gr.DrawLine(Pens.Green, 0, y, (iW - 1) * fontW, y);
            }

        }
    }
}

/*
note:

TextView служит для отображения редактируемого текста на форму.

Редактируемый текст предоставляется через интерфейс IGetTextData

        public int iTop=0;//верхняя строка
Номер первой строки

        public int iLeft=0;//левый символ
        public Font font = new Font("Consolas", 10);
        public int fontH, fontW;

        public int iH, iW;
        public Control owner;
        public TextData textData;

        public string[] buf;

        int lp;
        int fontW2;


*/
