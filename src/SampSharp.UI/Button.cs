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
using SampSharp.GameMode.Display;
using SampSharp.GameMode.SAMP;

namespace SampSharp.UI
{
    public class Button : Panel, ITextControl
    {
        private readonly BatchedPropertyCollection<Label> _properties = new BatchedPropertyCollection<Label>
        {
            ["Font"] = new BatchedProperty<Label, TextDrawFont>((t, v) => t.Font = v),
            ["ForeColor"] = new BatchedProperty<Label, Color>((t, v) => t.ForeColor = v),
            ["LetterSize"] = new BatchedProperty<Label, Vector2>((t, v) => t.LetterSize = v),
            ["Outline"] = new BatchedProperty<Label, int>((t, v) => t.Outline = v),
            ["Proportional"] = new BatchedProperty<Label, bool>((t, v) => t.Proportional = v),
            ["Shadow"] = new BatchedProperty<Label, int>((t, v) => t.Shadow = v),
            ["Text"] = new BatchedProperty<Label, string>((t, v) => t.Text = v),
        };
        
        private Label _label;

        public Button()
        {
            InitializeComponent();
        }

        #region Implementation of ITextControl

        public Color ForeColor
        {
            get { return _properties.Get<Color>(); }
            set
            {
                AssertNotDisposed();

                if (_properties.Set(value))
                {
                    OnPropertyChanged();
                    Invalidate();
                }
            }
        }

        public Vector2 LetterSize
        {
            get { return _properties.Get<Vector2>(); }
            set
            {
                AssertNotDisposed();

                if (_properties.Set(value))
                {
                    OnPropertyChanged();
                    Invalidate();
                }
            }
        }

        public int Outline
        {
            get { return _properties.Get<int>(); }
            set
            {
                AssertNotDisposed();

                if (_properties.Set(value))
                {
                    OnPropertyChanged();
                    Invalidate();
                }
            }
        }

        public TextDrawFont Font
        {
            get { return _properties.Get<TextDrawFont>(); }
            set
            {
                AssertNotDisposed();

                if (_properties.Set(value))
                {
                    OnPropertyChanged();
                    Invalidate();
                }
            }
        }

        public bool Proportional
        {
            get { return _properties.Get<bool>(); }
            set
            {
                AssertNotDisposed();

                if (_properties.Set(value))
                {
                    OnPropertyChanged();
                    Invalidate();
                }
            }
        }

        public int Shadow
        {
            get { return _properties.Get<int>(); }
            set
            {
                AssertNotDisposed();

                if (_properties.Set(value))
                {
                    OnPropertyChanged();
                    Invalidate();
                }
            }
        }

        public string Text
        {
            get { return _properties.Get<string>(); }
            set
            {
                AssertNotDisposed();

                if (_properties.Set(value))
                {
                    OnPropertyChanged();
                    Invalidate();
                }
            }
        }

        #endregion

        private void InitializeComponent()
        {
            _label = new Label();
            _label.Shadow = 0;
            _label.BackColor = 0x00000001;
            _label.LetterSize = new Vector2(0.25f, 1.3f);

            Controls.Add(_label);

            _properties.SetContainer(_label);
        }
        
        #region Overrides of Control

        protected override void OnRender()
        {
            base.OnRender();
            
            TextDraw.Selectable = true;

            _label.SuspendLayout();
            _properties.Apply();
            _label.ResumeLayout();

        }

        #endregion
    }
}