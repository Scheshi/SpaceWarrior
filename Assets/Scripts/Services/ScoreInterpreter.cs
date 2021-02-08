using System;
using UnityEngine.UI;

namespace Assets.Scripts.Services
{
    public class ScoreInterpreter
    {
        private readonly Text _text;

        public ScoreInterpreter(Text text)
        {
            _text = text;
        }

        public void Interpret(string value)
        {
            long integer;
            long.TryParse(value, out integer);
            _text.text = Calculate(integer);
        }

        private string Calculate(long integer)
        {
            long oldScore = 0;
            if (_text.text != String.Empty) long.TryParse(_text.text, out oldScore);
            integer += oldScore;
            if (integer < 0) throw new ArgumentException("zero in interpreter");
            if (integer >= 1_000_000_000) return (integer / 1_000_000_000) + "B";
            if (integer >= 1_000_000) return (integer / 1_000_000) + "M";
            if (integer >= 1000) return (integer / 1000) + "K";
            return integer.ToString();
        }
    }
}