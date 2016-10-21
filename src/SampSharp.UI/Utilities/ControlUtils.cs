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

namespace SampSharp.UI
{
    internal static class ControlUtils
    {
        private static readonly Dictionary<TextDrawFont, Dictionary<char, int>> CharacterWidths = new Dictionary
            <TextDrawFont, Dictionary<char, int>>
            {
                // Normal Font
                {
                    TextDrawFont.Normal,
                    new Dictionary<char, int>
                    {
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
                        { 'z', 17 },
                        { ' ', 15 }
                    }
                }
            };

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

        public static Vector2 GetTextSize(string text, TextDrawFont font, Vector2 letterSize, bool proportional = true)
        {
            return new Vector2(GetTextWidth(text, font, letterSize.X, proportional), GetTextHeight(letterSize.Y));
        }

        public static float GetTextWidth(string text, TextDrawFont font, float letterWidth, bool proportional = true)
        {
            if (string.IsNullOrEmpty(text))
                return 0;

            var fontCharacterWidths = CharacterWidths.ContainsKey(font)
                ? CharacterWidths[font]
                : new Dictionary<char, int>();

            var sz = 0.0f;
            var lines = text.Replace("~n~", "\n").Split('\n');

            foreach (var line in lines)
            {
                var lnSz = 0.0f;
                var sanitizedLine = SanitizeText(line);

                foreach (var c in sanitizedLine)
                {
                    int value;
                    if (!proportional || !fontCharacterWidths.TryGetValue(c, out value))
                        value = 20;
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
    }
}