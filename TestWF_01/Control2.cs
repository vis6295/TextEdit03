using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestWF_01
{
    class Control2: Control
    {
        public Control2()
        : base() 
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); //Если данный флаг установлен, контрол игнорирует сообщение WM_ERASEBKGND для уменьшения «шума» (flicker).Данный флаг должен быть установлен в комбинации с флагом UserPaint.    8192
            //SetStyle(ControlStyles.CacheText, true); //Кеширование текста контрола. При установке данного флага текст контола кэшируются.Кеширование увеличивает скорость отрисовки, но может порождать проблемы синхронизации. По умолчанию флаг сброшен.	16384
            SetStyle(ControlStyles.ContainerControl, false); //Данный флаг указывает на то, что контрол является контейнером.  1
            SetStyle(ControlStyles.DoubleBuffer, false); //Установка данного флага буферизирует отрисовку контрола, позволяя снизить мерцания и «шумы» отрисовки.  65536
            SetStyle(ControlStyles.EnableNotifyMessage, false); //Установка данного флага приводит к вызову OnNotifyMessage из WndProc при получении контролом сообщений. По умолчанию сброшен.   32768
                                                               //FixedHeight, FixedWidth Фиксированная высота / ширина контрола.При auto-scale высота / ширина не меняются.	64, 32
            SetStyle(ControlStyles.Opaque, false); //Прозрачность контрола.При установке флага подложка контрола не прорисовывается.    4
            SetStyle(ControlStyles.ResizeRedraw, false); //Отрисовка контрола при изменении его размеров.  16
                                                        //Selectable Контрол может получать фокус ввода.	512
                                                        //StandardClick Контрол использует стандартное поведение при щелчке мышью.	256
                                                        //StandardDoubleClick Контрол использует стандартное поведение при двойном щелчке мышью.  4096
            SetStyle(ControlStyles.SupportsTransparentBackColor, true); //Цвет фона контрола берется в качестве «прозрачного» цвета.Таким образом эмулируется прозрачность. Установка данного флага должна сопровождаться установкой флага. 2048
                                                                        //UserMouse Контрол самостоятельно обрабатывает события мыши, операционной системе не нужно обрабатывать события мыши данного контрола.	1024
            SetStyle(ControlStyles.UserPaint, true); //Контрол отрисовывает себя сам полностью сам.    2
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            log.msg("cntrl OnPaint" + e.ClipRectangle.ToString());
        }
        protected override void OnResize(EventArgs e)
        {
            log.msg($"cntrl OnResize w:{Width} h:{Height}");
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            log.msg($"cntrl OnSizeChanged w:{Width} h:{Height}");
        }
    }
}
