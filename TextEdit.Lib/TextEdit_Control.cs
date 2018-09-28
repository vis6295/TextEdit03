using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditLib
{
    partial class TextEdit
    {

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            log.msg(string.Empty + e.KeyChar + " #" + ((int)(e.KeyChar)).ToString()+" "+Char.IsSymbol(e.KeyChar).ToString());

            switch (e.KeyChar) {
                case (char)8: {//BackSpace
                        break;}
                case (char)27:{//Esc
                        break;}
                case (char)13:{//Enter
                        bufFlush();
                        break;}
                default: bufAppendChar(e.KeyChar); break;
            }
            //base.OnKeyPress(e);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up: SetCursor(pos.X, pos.Y - 1); break;
                case Keys.Down: SetCursor(pos.X, pos.Y + 1); break;
                case Keys.Left: SetCursor(pos.X - 1, pos.Y); break;
                case Keys.Right: SetCursor(pos.X + 1, pos.Y); break;
                case Keys.Delete: break;

                default: base.OnKeyDown(e); return;
            }
            bufFlush();
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                case Keys.Delete:
                    return true;
            }

            return base.IsInputKey(keyData);
        }

        enum CommandType
        {
            cmdNothing,
            cmdInsert,
            cmdDelete,
            cmdMove
        }

        enum Command {
            nothing,
            MoveUp,
            MoveDown,
            MoveLeft,
            MoveRight,
            Enter,
            Delete,
            BackSpace,
        }

        string bufLine="";
        int bufLen = 0;
        int bufPos = 0;

        CommandType lastCommand;

        //void SetCommandType(CommandType newCommand) {
        //    if (lastCommand != newCommand) {
        //        switch (lastCommand) {
        //            case CommandType.cmdInsert: {
        //                    textData.Insert(pos.Y + textView.iTop, bufPos, bufLine);
        //                    break;
        //                }
        //            case CommandType.cmdDelete:
        //                {
        //                    textData.Delete(pos.Y + textView.iTop, bufPos, bufLen);
        //                    break;
        //                }
        //        }
        //    }
        //}

        void ProcessCommand(Command command, char inputChar) {

        }

        void bufFlush() {
            if (lastCommand != CommandType.cmdNothing) {
                if (lastCommand == CommandType.cmdInsert) {
                    textData.Insert(pos.Y + textView.iTop, bufPos, bufLine);
                    pos.X += bufLine.Length;
                }
                //else if (lastCommand == CommandType.cmdDelete) textData.Delete(pos.Y + textView.iTop, bufPos, bufLen);
            }
            bufLine = ""; bufLen = 0; bufPos = pos.X;
            lastCommand = CommandType.cmdNothing;
        }

        void bufAppendChar(char ch) {
            if (lastCommand != CommandType.cmdInsert) {
                bufFlush();
                lastCommand = CommandType.cmdInsert;
                //bufLine = ""; bufLen = 0; bufPos = pos.X;
            }
            bufLine += ch;
        }
        //void bufDelete(bool delFlag)
        //{
        //    if (lastCommand != CommandType.cmdDelete)
        //    {
        //        bufFlush();
        //        lastCommand = CommandType.cmdDelete;
        //    }
        //    if (delFlag) {
        //        bufLen += 1;
        //    }
        //    else {//нажали бэкспэйс
        //        if (bufPos>0){//строки пока не удаляем
        //            bufPos -= 1;
        //            bufLen += 1;
        //        }
        //    }
        //}

    }
}
