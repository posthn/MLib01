using System;
using System.Text;

namespace MLib01.Text
{
    public sealed class TextEncoder
    {
        private int _keyValue;
        private int _keyLenght;
        private int[] _keyArr;
        public int Key { get => _keyValue; set { if (value < 10) throw new ArgumentException(); _keyValue = value; _keyLenght = value.ToString().Length; _keyArr = new int[_keyLenght]; } }

        private string _text;
        public string Text { get => _text; set { if (value == null) throw new NullReferenceException(); if (value.Length < 2) throw new ArgumentException(); _text = value; } }

        public TextEncoder(int key) { Key = key; GetKeyArr(ref _keyArr, _keyValue, _keyLenght); }
        public TextEncoder(int key, string text) : this(key) => Text = text;

        private static void GetKeyArr(ref int[] keyArr, int keyValue, int keyLenght)
        {
            int lastI = 0, tempSum = 0, tempN = 0;
            for (int i = 0; i < keyLenght; i++)
            {
                tempN = (int)System.Math.Pow(10, keyLenght - (i + 1));
                keyArr[i] = lastI = (keyValue - tempSum) / tempN;
                tempSum += tempN * lastI;
            }
        }

        private static int[] GetTempKeyArr(string text, int[] keyArr)
        {
            int[] resultArr = new int[text.Length];
            int step = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (i >= keyArr.Length + step) step += keyArr.Length;
                resultArr[i] = keyArr[i - step] + step - 1;
            }
            return resultArr;
        }

        private static string Encode(string text, int[] keyArr)
        {
            StringBuilder source = new StringBuilder(text);
            StringBuilder result = new StringBuilder(text);
            int[] tempKeyArr = GetTempKeyArr(text, keyArr);
            for (int i = 0; i < source.Length; i++)
                result[tempKeyArr[i]] = source[i];
            return result.ToString();
        }

        private static string Decode(string text, int[] keyArr)
        {
            StringBuilder soucre = new StringBuilder(text);
            StringBuilder result = new StringBuilder(text);
            int[] tempKeyArr = GetTempKeyArr(text, keyArr);
            for (int i = 0; i < soucre.Length; i++)
                result[i] = soucre[tempKeyArr[i]];
            return result.ToString();
        }

        public string GetEncryptedText() => Encode(_text, _keyArr);

        public string GetEncryptedText(string text) => Encode(text, _keyArr);

        public string GetDecryptedText() => Decode(_text, _keyArr);

        public string GetDecryptedText(string text) => Decode(text, _keyArr);
    }
}
