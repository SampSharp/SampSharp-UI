// SampSharp.UI
// Copyright 2016 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;

namespace SampSharp.UI.Utilities
{
    internal static class ControlHelper
    {
        #region Fonts

        private static readonly Dictionary<TextDrawFont, Font> CharacterWidths = new Dictionary
            <TextDrawFont, Font>
            {
                // Diploma Font
                {
                    TextDrawFont.Diploma,
                    new Font
                    {
                        Unprop = 27,
                        ReplacementSpaceChar = 10,
                        Charset =
                            new Dictionary<char, int>
                            {
                                { ' ', 12 },
                                { '!', 13 },
                                { '"', 13 },
                                { '£', 28 },
                                { '$', 28 },
                                { '%', 28 },
                                { '&', 28 },
                                { '\'', 8 },
                                { '(', 17 },
                                { ')', 17 },
                                { '*', 30 },
                                { '+', 28 },
                                { ',', 28 },
                                { '-', 12 },
                                { '.', 9 },
                                { '/', 21 },
                                { '0', 28 },
                                { '1', 14 },
                                { '2', 28 },
                                { '3', 28 },
                                { '4', 28 },
                                { '5', 28 },
                                { '6', 28 },
                                { '7', 28 },
                                { '8', 28 },
                                { '9', 28 },
                                { ':', 13 },
                                { ';', 13 },
                                { '<', 30 },
                                { '=', 30 },
                                { '>', 30 },
                                { '?', 30 },
//                        { '-', 10 },
                                { 'A', 25 },
                                { 'B', 23 },
                                { 'C', 21 },
                                { 'D', 24 },
                                { 'E', 22 },
                                { 'F', 20 },
                                { 'G', 24 },
                                { 'H', 24 },
                                { 'I', 17 },
                                { 'J', 20 },
                                { 'K', 22 },
                                { 'L', 20 },
                                { 'M', 30 },
                                { 'N', 27 },
                                { 'O', 27 },
                                { 'P', 26 },
                                { 'Q', 26 },
                                { 'R', 24 },
                                { 'S', 23 },
                                { 'T', 24 },
                                { 'U', 31 },
                                { 'V', 23 },
                                { 'W', 31 },
                                { 'X', 24 },
                                { 'Y', 23 },
                                { 'Z', 21 },
//                        { '&', 28 },
                                { '\\', 33 },
//                        { 'i', 14 },
                                { '_', 28 },
//                        { '!', 10 },
                                { 'a', 11 },
                                { 'b', 12 },
                                { 'c', 9 },
                                { 'd', 11 },
                                { 'e', 10 },
                                { 'f', 10 },
                                { 'g', 12 },
                                { 'h', 12 },
                                { 'i', 7 },
                                { 'j', 7 },
                                { 'k', 13 },
                                { 'l', 5 },
                                { 'm', 18 },
                                { 'n', 12 },
                                { 'o', 10 },
                                { 'p', 12 },
                                { 'q', 11 },
                                { 'r', 10 },
                                { 's', 12 },
                                { 't', 8 },
                                { 'u', 13 },
                                { 'v', 13 },
                                { 'w', 18 },
                                { 'x', 17 },
                                { 'y', 13 },
                                { 'z', 12 },
                                { 'À', 25 },
                                { 'Á', 25 },
                                { 'Â', 25 },
                                { 'Ã', 25 },
                                { 'Æ', 33 },
                                { 'Ç', 21 },
                                { 'È', 24 },
                                { 'É', 24 },
                                { 'Ê', 24 },
                                { 'Ë', 24 },
                                { 'Ì', 17 },
                                { 'Í', 17 },
                                { 'Î', 17 },
                                { 'Ï', 17 },
                                { 'Ò', 27 },
                                { 'Ó', 27 },
                                { 'Ô', 27 },
                                { 'Ö', 27 },
                                { 'Ù', 31 },
                                { 'Ú', 31 },
                                { 'Û', 31 },
                                { 'Ü', 31 },
                                { 'ß', 11 },
                                { 'à', 11 },
                                { 'á', 11 },
                                { 'â', 11 },
                                { 'ã', 11 },
                                { 'æ', 20 },
                                { 'ç', 9 },
                                { 'è', 10 },
                                { 'é', 10 },
                                { 'ê', 10 },
                                { 'ë', 10 },
                                { 'ì', 7 },
                                { 'í', 7 },
                                { 'î', 7 },
                                { 'ï', 7 },
                                { 'ò', 10 },
                                { 'ó', 10 },
                                { 'ô', 10 },
                                { 'ö', 10 },
                                { 'ù', 13 },
                                { 'ú', 13 },
                                { 'û', 13 },
                                { 'ü', 13 },
//                        { 'N', 27 },
//                        { 'n', 12 },
                                { '¿', 30 },
//                        { '0', 27 },
//                        { '1', 16 },
//                        { '2', 27 },
//                        { '3', 27 },
//                        { '4', 27 },
//                        { '5', 27 },
//                        { '6', 27 },
//                        { '7', 27 },
//                        { '8', 27 },
//                        { '9', 27 },
//                        { ':', 18 },
//                        { 'A', 29 },
//                        { 'B', 26 },
//                        { 'C', 25 },
//                        { 'D', 28 },
//                        { 'E', 26 },
//                        { 'F', 25 },
//                        { 'G', 27 },
//                        { 'H', 28 },
//                        { 'I', 12 },
//                        { 'J', 24 },
//                        { 'K', 25 },
//                        { 'L', 24 },
//                        { 'M', 30 },
//                        { 'N', 27 },
//                        { 'O', 29 },
//                        { 'P', 26 },
//                        { 'Q', 26 },
//                        { 'R', 25 },
//                        { 'S', 26 },
//                        { 'T', 25 },
//                        { 'U', 26 },
//                        { 'V', 28 },
//                        { 'W', 32 },
//                        { 'X', 27 },
//                        { 'Y', 26 },
//                        { 'Z', 26 },
//                        { 'À', 29 },
//                        { 'Á', 29 },
//                        { 'Â', 29 },
//                        { 'Ã', 29 },
//                        { 'Æ', 33 },
//                        { 'Ç', 25 },
//                        { 'È', 26 },
//                        { 'É', 26 },
//                        { 'Ê', 26 },
//                        { 'Ë', 26 },
//                        { 'Ì', 14 },
//                        { 'Í', 14 },
//                        { 'Î', 14 },
//                        { 'Ï', 14 },
//                        { 'Ò', 29 },
//                        { 'Ó', 29 },
//                        { 'Ô', 29 },
//                        { 'Ö', 29 },
//                        { 'Ù', 26 },
//                        { 'Ú', 26 },
//                        { 'Û', 26 },
//                        { 'Ü', 26 },
//                        { 'ß', 21 },
                                { 'Ñ', 25 }
                            }
                    }
                },
                // Normal Font
                {
                    TextDrawFont.Normal,
                    new Font
                    {
                        Unprop = 20,
                        ReplacementSpaceChar = 10,
                        Charset =
                            new Dictionary<char, int>
                            {
                                { ' ', 15 },
                                { 'A', 22 },
                                { 'B', 19 },
                                { 'C', 19 },
                                { 'D', 22 },
                                { 'E', 16 },
                                { 'F', 19 },
                                { 'G', 24 },
                                { 'H', 22 },
                                { 'I', 11 },
                                { 'J', 16 },
                                { 'K', 21 },
                                { 'L', 15 },
                                { 'M', 28 },
                                { 'N', 24 },
                                { 'O', 27 },
                                { 'P', 20 },
                                { 'Q', 25 },
                                { 'R', 19 },
                                { 'S', 19 },
                                { 'T', 18 },
                                { 'U', 23 },
                                { 'V', 23 },
                                { 'W', 31 },
                                { 'X', 23 },
                                { 'Y', 19 },
                                { 'Z', 21 },
                                { '\'', 12 },
                                { '&', 23 },
                                { '%', 34 },
                                { '$', 20 },
                                { '£', 27 },
                                { '"', 17 },
                                { '!', 9 },
                                { '/', 15 },
                                { '.', 12 },
                                { '-', 14 },
                                { ',', 12 },
                                { '+', 20 },
                                { '*', 21 },
                                { ')', 12 },
                                { '(', 12 },
                                { '7', 21 },
                                { '6', 21 },
                                { '5', 21 },
                                { '4', 21 },
                                { '3', 21 },
                                { '2', 21 },
                                { '1', 15 },
                                { '0', 23 },
                                { '?', 19 },
                                { '>', 24 },
                                { '=', 24 },
                                { '<', 24 },
                                { ';', 12 },
                                { ':', 12 },
                                { '9', 21 },
                                { '8', 20 },
                                { '_', 21 },
                                { 'a', 19 },
                                { 'b', 20 },
                                { 'c', 14 },
                                { 'd', 20 },
                                { 'e', 19 },
                                { 'f', 13 },
                                { 'g', 20 },
                                { 'h', 19 },
                                { 'i', 9 },
                                { 'j', 9 },
                                { 'k', 19 },
                                { 'l', 9 },
                                { 'm', 29 },
                                { 'n', 19 },
                                { 'o', 21 },
                                { 'p', 19 },
                                { 'q', 19 },
                                { 'r', 15 },
                                { 's', 15 },
                                { 't', 14 },
                                { 'u', 18 },
                                { 'v', 19 },
                                { 'w', 27 },
                                { 'x', 20 },
                                { 'y', 20 },
                                { 'z', 17 }
                            }
                    }
                }
            };

        #endregion

        public static string SanitizeText(string text)
        {
            var start = 0;
            while ((start = text.IndexOf('~', start)) != -1)
            {
                var end = text.IndexOf('~', start + 1) + 1;

                if (end == -1)
                    return text;

                text = text.Remove(start, end - start);
            }

            text = text.Replace("{", "(");
            text = text.Replace("}", ")");
            text = text.Replace("@", "(at)");

            return text;
        }

        private static int GetStringWithMaxWidth(string value, TextDrawFont font, Vector2 letterSize, bool proportional,
            float width)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            var len = 1;
            var lastSpace = 1;
            while (value.Length > len)
            {
                var cur = GetTextWidth(value.Substring(0, len + 1), font, letterSize.X, proportional);
                if (cur > width && value[len] != ' ')
                    return len*0.75f < lastSpace ? lastSpace + 1 : len;

                if (value[len] == ' ')
                    lastSpace = len;
                len++;
            }

            return value.Length;
        }

        public static string FitTextInWidth(string value, TextDrawFont font, Vector2 letterSize, bool proportional,
            float maxWidth)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;
            if ((letterSize.X <= 0) && proportional)
                return value;

            var lines = new List<string>();

            foreach (var ln in value.Replace("~n~", "\n").Split('\n'))
            {
                var line = ln;
                if (string.IsNullOrWhiteSpace(line))
                    lines.Add(string.Empty);
                else
                    while (!string.IsNullOrWhiteSpace(line))
                    {
                        var len = GetStringWithMaxWidth(line, font, letterSize, proportional, maxWidth);

                        lines.Add(line.Substring(0, len));

                        line = line.Length > len ? line.Substring(len) : null;
                    }
            }

            return string.Join("\n", lines);
        }

        public static Vector2 GetTextSize(string text, TextDrawFont font, Vector2 letterSize, bool proportional = true)
        {
            return new Vector2(GetTextWidth(text, font, letterSize.X, proportional), GetTextHeight(letterSize.Y));
        }

        public static float GetTextWidth(string text, TextDrawFont font, float letterWidth, bool proportional = true)
        {
            if (string.IsNullOrEmpty(text))
                return 0;

            var fnt = CharacterWidths.ContainsKey(font)
                ? CharacterWidths[font]
                : new Font { Charset = new Dictionary<char, int>(), ReplacementSpaceChar = 10, Unprop = 20 };

            var sz = 0.0f;
            var lines = text.Replace("~n~", "\n").Split('\n');

            foreach (var line in lines)
            {
                var lnSz = 0.0f;
                var sanitizedLine = SanitizeText(line);

                foreach (var c in sanitizedLine)
                {
                    int value;
                    if (!proportional || !fnt.Charset.TryGetValue(c, out value))
                        value = fnt.Unprop;
                    lnSz += value*letterWidth;
                }

                sz = Math.Max(sz, lnSz);
            }

            return sz;
        }

        public static float GetTextHeight(float letterHeight)
        {
            return letterHeight*12.0f;
        }

        private struct Font
        {
            public Dictionary<char, int> Charset;
            public int Unprop;
            public int ReplacementSpaceChar;
        }
    }
}