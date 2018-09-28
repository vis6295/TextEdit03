using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditLib
{
    public class Cursor
    {
        public TextView textView;
        public TextEdit owner;

        bool _visible = false;
        bool _hideFlag = false;
        private Point _position;

        public bool visible { get { return _visible;} set { _visible = value; if (_visible) Show(); else Hide(); } }
        public Point position { set { SetPosition(value); } get { return _position; } }

        private void SetPosition(Point value)
        {
            int newX = (value.X < 0) ? 0 : (value.X<textView.iW)? value.X: textView.iW-1;
            int newY = (value.Y < 0) ? 0 : (value.Y < textView.iH) ? value.Y : textView.iH - 1;

            //if (newX != _position.X || newY != _position.Y) {
            //    bool old_visible = visible; visib
            //    _position = new Point(newX, newY);
            //    if (_visible && !_hideFlag) DrawVisible();
            //}
        }


        const int visibleTicks = 10;
        const int hideTicks = 10;

        public void TimerTick() {
            if (_visible) {
                //if ()
            }
        }

        /// <summary>
        /// курсор становиться видимым
        /// </summary>
        public void Show() { }
        /// <summary>
        /// курсор становиться не видимым
        /// </summary>
        public void Hide() { }
        /// <summary>
        /// рисуется видимым
        /// </summary>
        public void DrawVisible() { }
        /// <summary>
        /// рисуется скрытым
        /// </summary>
        public void DrawHide() { }
    }
}
