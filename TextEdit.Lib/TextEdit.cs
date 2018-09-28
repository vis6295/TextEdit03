using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditLib
{
    public partial class TextEdit: Control
    {
        //25.09.2018.1
        Bitmap bitmap = null;

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, new Point(0, 0));

            if (visibleCursor)  textView.PaintChar(e.Graphics, pos.X, pos.Y, Color.White, Color.Blue);
        }

        protected override void OnResize(EventArgs e)
        {
            //log.msg("OnResize");
            base.OnResize(e);
            this.CreateGraphics().DrawImage(bitmap, new Point(0, 0));
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            //log.msg("OnSizeChanged");
            bitmap = new Bitmap(Width, Height);
            //TextRenderer.DrawText(gr, "Text edit", new Font("Times New Roman", 24), new Point(0, 0), Color.Green);
            textView.Resize();
            textView.Paint(Graphics.FromImage(bitmap));

            base.OnSizeChanged(e);
        }

        //25.09.2018.2

        public TextView textView;
        public TextData textData;

        //public TextEdit() {
        //    textView = new TextView(this);
        //}

        /*
         Таблица 5. Флаги ControlStyles.

Наименование флага	Описание	Значение
AllPaintingInWmPaint	Если данный флаг установлен, контрол игнорирует сообщение WM_ERASEBKGND для уменьшения «шума» (flicker). Данный флаг должен быть установлен в комбинации с флагом UserPaint.	8192
CacheText	Кеширование текста контрола. При установке данного флага текст контола кэшируются. Кеширование увеличивает скорость отрисовки, но может порождать проблемы синхронизации. По умолчанию флаг сброшен.	16384
ContainerControl	Данный флаг указывает на то, что контрол является контейнером.	1
DoubleBuffer	Установка данного флага буферизирует отрисовку контрола, позволяя снизить мерцания и «шумы» отрисовки.	65536
EnableNotifyMessage	Установка данного флага приводит к вызову OnNotifyMessage из WndProc при получении контролом сообщений. По умолчанию сброшен.	32768
FixedHeight, FixedWidth	Фиксированная высота/ширина контрола. При auto-scale высота/ширина не меняются.	64, 32
Opaque	Прозрачность контрола. При установке флага подложка контрола не прорисовывается.	4
ResizeRedraw	Отрисовка контрола при изменении его размеров.	16
Selectable	Контрол может получать фокус ввода.	512
StandardClick	Контрол использует стандартное поведение при щелчке мышью.	256
StandardDoubleClick	Контрол использует стандартное поведение при двойном щелчке мышью.	4096
SupportsTransparentBackColor	Цвет фона контрола берется в качестве «прозрачного» цвета. Таким образом эмулируется прозрачность. Установка данного флага должна сопровождаться установкой флага .	2048
UserMouse	Контрол самостоятельно обрабатывает события мыши, операционной системе не нужно обрабатывать события мыши данного контрола.	1024
UserPaint	Контрол отрисовывает себя сам полностью сам.	2

         */

        Timer timer = new Timer();

        public TextEdit(TextData textData)
        {
            //ResizeRedraw = true;
            //ResizeRedraw = false;
            //TabStop = true;
            //TabIndex = 99;
            //Enabled = true;
            //SelectionMode.
            //this.
            this.textData = textData;
            this.textData.ChangeLine += OnChangeLine;

            textView = new TextView(this);

            SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //SetStyle(ControlStyles.EnableNotifyMessage, true);
            SetStyle(ControlStyles.ResizeRedraw, false);
            //SetStyle(ControlStyles.StandardClick, true);
            SetStyle(ControlStyles.UserMouse, false);

            timer.Interval = 200;
            timer.Tick += OnTimer;
            timer.Enabled = true;
        }

        // временное решение. Потом в массиве textView строки нужно заменить соответствующими записями и там обновлять.
        List<int> updateList = new List<int>();
        object lockObject = new object();
        void OnChangeLine(int numStr)
        {
            if (numStr >= textView.iTop && numStr < textView.iTop + textView.iH) {
                lock (lockObject) {
                    if (!updateList.Contains(numStr)) updateList.Add(numStr);
                }
            }
        }

        void OnTimer(object sender, EventArgs e)
        {
            if (updateList.Count > 0) {
                List<int> old;
                List<int> tmp = new List<int>();
                lock (lockObject)
                {
                    old = updateList;
                    updateList = tmp;
                }

                foreach (int lineNum in old) {
                    textView.buf[lineNum - textView.iTop] = textData.GetLine(lineNum);
                }

                textView.Paint(Graphics.FromImage(bitmap));

                Graphics gr = CreateGraphics();
                gr.DrawImage(bitmap, new Point(0, 0));

                if (visibleCursor) textView.PaintChar(gr, pos.X, pos.Y, Color.White, Color.Blue);
            }

            bufFlush();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            //log.msg("OnMouseClick");
            if (!Focused) Focus();          
            else base.OnMouseClick(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            VisibleCursor = true;
        }
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            VisibleCursor = false;
        }

        //protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        //{
        //    log.msg("OnPreviewKeyDown");
        //    //if (Array.IndexOf<Keys>(InputKeys, e.KeyCode) != -1)
        //    //{
        //    //}
        //    e.IsInputKey = true;
        //    base.OnPreviewKeyDown(e);
        //}

    }



    public delegate void LogMsg(string str);

    public static class log {
        public static event LogMsg logMsg;
        public static event LogMsg logMsg2;
        public static void msg(string str) {
            if (logMsg!=null)  logMsg(str);
        }
        public static void msg2(string str)
        {
            if (logMsg2 != null) logMsg2(str);
        }
    }
}
