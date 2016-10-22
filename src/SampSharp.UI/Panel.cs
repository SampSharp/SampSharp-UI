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

using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;
using SampSharp.UI.Utilities;

namespace SampSharp.UI
{
    public class Panel : ContainerControl
    {
        private readonly BatchedProperty<TextDrawControl, Color> _backColor =
            new BatchedProperty<TextDrawControl, Color>((t, v) => t.BoxColor = v);

        public Panel()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            _backColor.Set(0x00000033);

            TextDraw = new TextDrawControl();
            TextDraw.AssignParent(this);

            _backColor.SetContainer(TextDraw);
        }

        public TextDrawControl TextDraw { get; private set; }

        public virtual Color BoxColor
        {
            get { return _backColor.Get(); }
            set
            {
                AssertNotDisposed();

                if (_backColor.Set(value))
                {
                    OnPropertyChanged();
                    Invalidate();
                }
            }
        }
        
        #region Overrides of Control

        protected override void OnRender()
        {
            _backColor.Apply();

            TextDraw.SuspendLayout();
            TextDraw.Position = new Vector2(4, 4);
            TextDraw.Text = "_";
            TextDraw.Font = TextDrawFont.Normal;
            TextDraw.UseBox = true;
            TextDraw.Proportional = false;
            TextDraw.LetterSize = new Vector2(1, (Size.Y - 7) / 10);
            TextDraw.TextSize = new Vector2(GetAbsolutePosition().X + Size.X - 4, Size.Y - 0);

            TextDraw.ResumeLayout();

            base.OnRender();
        }

        #endregion
    }
}