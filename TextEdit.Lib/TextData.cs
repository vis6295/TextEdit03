using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditLib
{
    interface ITextData
    {
        /// <summary>
        /// вставляет простой блок
        /// </summary>
        /// <param name="numStr"></param>
        /// <param name="pos"></param>
        /// <param name="data"></param>
        void Insert(int numStr, int pos, string data);

        /// <summary>
        /// добавляет новую строку перед numStr
        /// </summary>
        /// <param name="numStr"></param>
        void Insert(int numStr);

        /// <summary>
        /// удаляет простой блок
        /// </summary>
        /// <param name="numStr"></param>
        /// <param name="pos"></param>
        /// <param name="data"></param>
        void Delete(int numStr, int pos, int len);

        /// <summary>
        /// Удаляет строку целиком
        /// </summary>
        /// <param name="numStr"></param>
        void Delete(int numStr);

        /// <summary>
        /// Заменяет простой блок в строке
        /// </summary>
        /// <param name="numStr"></param>
        /// <param name="pos"></param>
        /// <param name="data"></param>
        void Replace(int numStr, int pos, string data);
    }

    /// <summary>
    /// Хранилище текстовых строк
    /// интерфейс рассчитан на обработку простых блоков (без перевода строк)
    /// </summary>
    public class TextData: ITextData
    {
        public int Count { get { return data.Count; } }

        List<string> data = null;

        public TextData()
        {
            data = new List<string>();
        }

        public void Load(string FilePath)
        {
            data.Clear();
            foreach (string s in File.ReadAllLines(FilePath)) data.Add(s);
        }

        public void Save(string FilePath)
        {
            File.WriteAllLines(FilePath, data.ToArray());
        }

        /* Многостроковые блоки попозже реализуем
         * 
         * 
        /// <summary>
        /// Многострочный текст. Необходима разбивка на строки
        /// </summary>
        /// <param name="numStr"></param>
        /// <param name="pos"></param>
        /// <param name="data"></param>
        public void InsertText(int numStr, int pos, string data)
        {
        }

        /// <summary>
        /// добавляет новую строку перед numStr
        /// </summary>
        /// <param name="numStr"></param>
        public void Insert(int numStr)
        {
            data.Insert(numStr, "");
        }

        /// <summary>
        /// Удаляет блок
        /// </summary>
        /// <param name="numStr1"></param>
        /// <param name="pos1"></param>
        /// <param name="numStr2"></param>
        /// <param name="pos2"></param>
        public void Delete(int numStr1, int pos1, int numStr2, int pos2)
        {
        }
        */

        /// <summary>
        /// возвращает строку №
        /// </summary>
        /// <param name="numStr"></param>
        /// <returns></returns>
        public string GetLine(int numStr)
        {
            if (data.Count > numStr) return data[numStr];
            else return null;
        }

        void ITextData.Insert(int numStr, int pos, string str)
        {
            data[numStr] = data[numStr].Insert(pos, str);
        }

        void ITextData.Insert(int numStr)
        {
            data.Insert(numStr, "");
        }

        void ITextData.Delete(int numStr, int pos, int len)
        {
            data[numStr] = data[numStr].Remove(pos, len);
        }

        void ITextData.Delete(int numStr)
        {
            data.RemoveAt(numStr);
        }

        void ITextData.Replace(int numStr, int pos, string str)
        {
            string tmp = data[numStr];
            data[numStr] = string.Concat(tmp.Substring(0, pos), str, tmp.Substring(pos+str.Length));
        }
    }
}
