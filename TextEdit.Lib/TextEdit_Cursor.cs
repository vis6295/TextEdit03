using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditLib
{
    partial class TextEdit
    {
        Point pos;
        void SetCursor(int x, int y) {
            //log.msg("SetCursor");
            int newX = (x < 0) ? 0 : (x < textView.iW) ? x : textView.iW - 1;
            int newY = (y < 0) ? 0 : (y < textView.iH) ? y : textView.iH - 1;
            if (pos.X != newX || pos.Y != newY) {
                Graphics graphics = CreateGraphics();
                textView.PaintChar(graphics, pos.X, pos.Y);
                textView.PaintChar(graphics, newX, newY, Color.White, Color.Blue);
                pos = new Point(newX, newY);
            }
        }

        void ShowCursor() {
            Graphics graphics = CreateGraphics();
            textView.PaintChar(graphics, pos.X, pos.Y, Color.White, Color.Blue);
        }
        void HideCursor()
        {
            Graphics graphics = CreateGraphics();
            textView.PaintChar(graphics, pos.X, pos.Y);
        }

        bool visibleCursor = false;

        public bool VisibleCursor {
            get { return visibleCursor; }
            set { if (visibleCursor != value) { if (value) ShowCursor(); else HideCursor(); visibleCursor = value; }  } }

    }
}
