using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditLib
{
    public class TextView
    {
        public Font font = new Font("Consolas", 10);
        public char EmptyChar = '.';//символ - заполнитель
        public Brush DefaultBrush = Brushes.GreenYellow;//для отображения шрифта
        public Color DefaultBkColor = Color.Black;
        public Brush bkBrush = Brushes.Black;

        public int iTop=0;//верхняя строка
        public int iLeft=0;//левый символ

        public float fontH, fontW, extW, extH;//размеры шрифра

        public int iH, iW;

        TextEdit owner;

        public string[] buf;

        //public bool validBuf = false;

        //public TextView(Control owner, TextData textData) {

        //    Graphics gr = Graphics.FromHwnd(IntPtr.Zero);
        //    SizeF pt1 = gr.MeasureString("*", font);
        //    SizeF pt2 = gr.MeasureString("**\n**", font);

        //    fontW = pt2.Width - pt1.Width;
        //    extW = pt1.Width - fontW;

        //    fontH = pt2.Height - pt1.Height;
        //    extH = pt1.Height - fontH;

        //    //fontH = font.GetHeight();
        //    //fontW = (int)Math.Ceiling(font.SizeInPoints);

        //    //this.owner = owner;
        //    //iH = (int)(this.owner.Height / fontH);
        //    //iW = (int)((this.owner.Width - extW) / fontW);

        //    //this.textData = textData;
        //    //buf = new string[iH];
        //    //for (int i = 0; i < iH; i++) buf[i] = this.textData.GetLine(i);
        //}

        //StringFormat fmt = StringFormat.GenericTypographic;

        public TextView(TextEdit owner)
        {
            //fmt.FormatFlags|=StringFormatFlags.

            //Brush brush = (Brush)Brushes.Green.Clone();
            //System.Drawing.SolidBrush myBrush = new System.Drawing..SolidBrush(System.Drawing.Color.Red);

            iTop = 0;
            iLeft = 0;

            Graphics gr = Graphics.FromHwnd(IntPtr.Zero);
            SizeF pt1 = gr.MeasureString("*", font, new PointF(0,0), StringFormat.GenericTypographic);
            SizeF pt2 = gr.MeasureString("**\n**", font, new PointF(0, 0), StringFormat.GenericTypographic);

            fontW = pt2.Width - pt1.Width;
            extW = pt1.Width - fontW;

            fontH = pt2.Height - pt1.Height;
            extH = pt1.Height - fontH;

            this.owner = owner;

            //fontH = font.GetHeight();
            //fontW = (int)Math.Ceiling(font.SizeInPoints);

            /*
            iH = (int)(this.owner.Height / fontH);
            iW = (int)((this.owner.Width - extW) / fontW);

            this.textData = textData;
            buf = new string[iH];
            for (int i = 0; i < iH; i++) buf[i] = this.textData.GetLine(i);
            */
        }

        /// <summary>
        /// Выделяет строку для отображения. те. длиной iW, при необходимости заполняется EmptyChar 
        /// </summary>
        /// <param name="idx">номер строки в буфере</param>
        /// <returns></returns>
        string GetString(int idx) {
            string result = "";
            int aLen = (buf[idx] == null) ? 0 : buf[idx].Length;
            if (aLen > iLeft) result = buf[idx].Substring(iLeft, ((iW < aLen - iLeft) ? iW : aLen - iLeft));
            int ext = iW - result.Length;
            if (ext > 0) result += new string(EmptyChar, ext);
            return result;
        }

        /// <summary>
        /// Возвращает символ, расположенный в соответствующих координатах
        /// </summary>
        /// <param name="ix"></param>
        /// <param name="iy"></param>
        /// <returns></returns>
        public char GetChar(int idx, int idy)
        {
            if (buf[idy]==null || buf[idy].Length<=iLeft+idx) return EmptyChar;
            return buf[idy][iLeft + idx];
        }

        internal void Resize()
        {
            int new_iH = (int)(this.owner.Height / fontH);
            int new_iW = (int)(this.owner.Width / fontW);

            if (new_iH > iH)
            {
                string[] new_buf = new string[new_iH];
                for (int i = 0; i < iH; i++)
                {
                    new_buf[i] = buf[i];
                }
                for (int i = iH; i < new_iH; i++)
                {
                    new_buf[i] = owner.textData.GetLine(i + iTop);
                }
                buf = new_buf;
            }
            else if (new_iH < iH) {
                string[] new_buf = new string[new_iH];
                for (int i = 0; i < new_iH; i++) {
                    new_buf[i] = buf[i];
                }
                buf = new_buf;
            }

            iH = new_iH;
            iW = new_iW;
        }


        public void Paint(Graphics gr)
        {
            gr.Clear(DefaultBkColor);
            for (int i = 0; i < iH; i++) gr.DrawString(GetString(i), font, DefaultBrush, 0, i * fontH, StringFormat.GenericTypographic);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="ix"></param>
        /// <param name="iy"></param>
        public void PaintChar(Graphics graphics, int idx, int idy) {
            PointF pos = new PointF(fontW * idx, fontH * idy);
            graphics.FillRectangle(bkBrush, pos.X, pos.Y, fontW, fontH);
            graphics.DrawString(string.Empty + GetChar(idx, idy), font, DefaultBrush, pos, StringFormat.GenericTypographic);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="ix"></param>
        /// <param name="iy"></param>
        /// <param name="color"></param>
        /// <param name="bkColor"></param>
        public void PaintChar(Graphics graphics, int idx, int idy, Color color, Color bkColor)
        {
            using (Brush bkBrush = new SolidBrush(bkColor))
            using (Brush fontBrush = new SolidBrush(color))
            {
                PointF pos = new PointF(fontW * idx, fontH * idy);
                graphics.FillRectangle(bkBrush, pos.X, pos.Y, fontW, fontH);
                graphics.DrawString(string.Empty + GetChar(idx, idy), font, fontBrush, pos, StringFormat.GenericTypographic);
            }
        }


        /*
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
        */
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
