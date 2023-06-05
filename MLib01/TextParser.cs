using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MLib01.Text
{
    public class TextParser
    {
        #region Separators
        private static IEnumerable<char> _textSeparators, _sentenceSeparators;
        public static IEnumerable<char> TextSeparators { get => _textSeparators; set { if (value == null | value?.Count() < 1) throw new ArgumentNullException(); _textSeparators = value; } }
        public static IEnumerable<char> SentenceSeparators { get => _sentenceSeparators; set { if (value == null | value?.Count() < 1) throw new ArgumentNullException(); _sentenceSeparators = value; } }

        public static void SetDefaultSeparators() { _textSeparators = new char[] { '.', '!', '?' }; _sentenceSeparators = new char[] { ',', ';', '-', ':', ' ' }; }
        static TextParser() => SetDefaultSeparators();
        #endregion
    
        private DisplayMode _displayMode;
        public DisplayMode DisplayMode { get => _displayMode; set => _displayMode = value; }

        private string _text;
        public string Text { get => _text; set { if (value == null | value?.Length < 1) throw new ArgumentNullException(); _text = value; TextFormat(ref _text, _textSeparators.ToArray()); Parse(ref _sentences, _text); } }

        private List<string> _sentences;
        public int Lenght { get => _sentences.Count; }

        public string this[int index] { get { if (index < 0 | index > _sentences.Count - 1) throw new ArgumentOutOfRangeException(); return _sentences[index]; } }

        public TextParser() { _displayMode = DisplayMode.Standart; _sentences = new List<string>(); }
        public TextParser(DisplayMode displayMode) : this() => _displayMode = displayMode;
        public TextParser(string text) : this() => Text = text;
        public TextParser(string text, DisplayMode displayMode) : this(text) => _displayMode = displayMode;

        #region Impl
        private static void TextFormat(ref string text, char[] textSeparators)
        {
            if (!CheckingTheSuitabilityOfTheText(text, textSeparators.ToArray())) throw new ArgumentException();
            StringBuilder textFormater = new StringBuilder(text);
            while (textFormater[0] == ' ')
                textFormater.Remove(0, 1);
            while (textFormater.ToString().Contains("  "))
                textFormater.Replace("  ", " ");
            foreach (var separator in textSeparators)
            { textFormater.Replace(separator + " ", separator + "/"); textFormater.Replace(separator + "", separator + "/"); }
            text = textFormater.ToString();
        }

        private static void SentenceFormat(ref string sentence, char[] sentenceSeparators)
        {
            if (!CheckingTheSuitabilityOfTheText(sentence, sentenceSeparators.ToArray())) throw new ArgumentException();
            StringBuilder sentenceFormater = new StringBuilder(sentence);
            while (sentenceFormater.ToString().Contains("  "))
                sentenceFormater.Replace("  ", " ");
            foreach (var separator in sentenceSeparators)
            { sentenceFormater.Replace(separator + " ", "/"); sentenceFormater.Replace("  ", " "); sentenceFormater.Replace(separator, '/'); }
            sentence = sentenceFormater.ToString().ToLower();
        }

        private static void Parse(ref List<string> list, string stringValue)
        {
            StringBuilder sentenceBuilder = new StringBuilder();
            foreach (var str in stringValue.Split(new char[] { '/' }))
            {
                if (str.Length < 1) continue;
                sentenceBuilder.Append(str);
                while (sentenceBuilder?[0] == ' ')
                    sentenceBuilder.Remove(0, 1);
                list.Add(sentenceBuilder.ToString());
                sentenceBuilder.Clear();
            }
        }

        private static bool CheckingTheSuitabilityOfTheText(string text, char[] separators)
        {
            foreach (var separator in separators)
                if (text.Contains(separator)) return true;
            return false;
        }
        #endregion

        public void Parse(string text) => Text = text;

        public List<string> GetSentences()
        {
            List<string> resultList = new List<string>();
            _sentences.ForEach(sentence => resultList.Add(sentence));
            return resultList;
        }

        public List<string> GetSentences(string text)
        {
            Text = text;
            List<string> resultList = GetSentences();
            _text = null;
            _sentences.Clear();
            return resultList;
        }

        public List<string> GetWords(int sentenceIndex)
        {
            if (sentenceIndex < 0 | sentenceIndex > Lenght - 1) throw new ArgumentOutOfRangeException();
            List<string> resultList = new List<string>();
            string targetSentence = this[sentenceIndex];
            SentenceFormat(ref targetSentence, _sentenceSeparators.Concat(_textSeparators).ToArray());
            Parse(ref resultList, targetSentence);
            return resultList;
        }

        public List<List<string>> GetWords()
        {
            List<List<string>> resultList = new List<List<string>>();
            for (int i = 0; i < _sentences.Count; i++)
                resultList.Add(GetWords(i));
            return resultList;
        }

        public void Clear() => _sentences.Clear();

        public override string ToString()
        {
            if (_sentences.Count < 1) return null;
            string result = "";
            switch (_displayMode)
            {
                case DisplayMode.Standart:
                    _sentences.ForEach(sentence => result += (" " + sentence));
                    return result;
                case DisplayMode.LineByLine:
                    for (int i = 0; i < _sentences.Count; i++)
                    { result += this[i]; if (i < _sentences.Count - 1) result += Environment.NewLine; }
                    return result;
                default:
                    return null;
            }
        }
    }

    public enum DisplayMode
    {
        Standart,
        LineByLine
    }
}
