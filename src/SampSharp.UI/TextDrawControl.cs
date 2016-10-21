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
using System.Diagnostics;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.SAMP;
using SampSharp.UI.Utilities;

namespace SampSharp.UI
{
    public class TextDrawControl : Control, ITextControl, IPreviewControl, ISelectableControl
    {
        private readonly BatchedPropertyCollection<PlayerTextDraw> _properties = new BatchedPropertyCollection<PlayerTextDraw>
        {
            // TODO some  default values are incorrect, need to find correct values
            ["Alignment"] = new BatchedProperty<PlayerTextDraw,TextDrawAlignment>((t, v) => t.Alignment = v, TextDrawAlignment.Left),
            ["BackColor"] = new BatchedProperty<PlayerTextDraw,Color>((t, v) => t.BackColor = v,  Color.Black),// TODO
            ["BoxColor"] = new BatchedProperty<PlayerTextDraw,Color>((t, v) => t.BoxColor = v, Color.Black),// TODO
            ["Font"] = new BatchedProperty<PlayerTextDraw,TextDrawFont>((t, v) => t.Font = v, TextDrawFont.Normal),
            ["ForeColor"] = new BatchedProperty<PlayerTextDraw,Color>((t, v) => t.ForeColor = v, Color.White),// TODO
            ["LetterSize"] = new BatchedProperty<PlayerTextDraw, Vector2>((t, v) => t.LetterSize = v, Vector2.One),// TODO
            ["Outline"] = new BatchedProperty<PlayerTextDraw, int>((t, v) => t.Outline = v, 1),// TODO
            ["Position"] = new BatchedProperty<PlayerTextDraw, Vector2>((t, v) => t.Position = v, Vector2.Zero),
            ["PreviewModel"] = new BatchedProperty<PlayerTextDraw, int>((t, v) => t.PreviewModel = v, 0),// TODO
            ["PreviewPrimaryColor"] = new BatchedProperty<PlayerTextDraw, int>((t, v) => t.PreviewPrimaryColor = v, -1),
            ["PreviewSecondaryColor"] = new BatchedProperty<PlayerTextDraw, int>((t, v) => t.PreviewSecondaryColor = v, -1),
            ["PreviewRotation"] = new BatchedProperty<PlayerTextDraw, Vector3>((t, v) => t.PreviewRotation = v, Vector3.Zero),// TODO
            ["PreviewZoom"] = new BatchedProperty<PlayerTextDraw, float>((t, v) => t.PreviewZoom = v, 0),// TODO
            ["Proportional"] = new BatchedProperty<PlayerTextDraw, bool>((t, v) => t.Proportional = v, true),
            ["Selectable"] = new BatchedProperty<PlayerTextDraw, bool>((t, v) => t.Selectable = v, false),
            ["Shadow"] = new BatchedProperty<PlayerTextDraw, int>((t, v) => t.Shadow = v, 1),// TODO
            ["Text"] = new BatchedProperty<PlayerTextDraw, string>((t, v) => t.Text = v.Replace("\n", "~n~"), "_"),
            ["UseBox"] = new BatchedProperty<PlayerTextDraw, bool>((t, v) => t.UseBox = v, false),
            ["TextSize"] = new BatchedProperty<PlayerTextDraw, Vector2>((t, v) =>
            {
                t.Width = v.X;
                t.Height = v.Y;
            }, Vector2.Zero),
        };
        
        private PlayerTextDraw _textDraw;
        
        private void CheckSize()
        {
            var size = ControlUtils.GetTextSize(Text, Font, LetterSize, Proportional); // + ?
            // TODO better calculation
            // TODO override Size

            if (Size != size)
                Size = size;
        }

        private void CheckTextDrawExistance()
        {
            if ((_textDraw == null && Owner != null) || (_textDraw != null && Owner != _textDraw.Owner))
            {
                _textDraw = new PlayerTextDraw(Owner);
                _textDraw.Click += TextDrawOnClick;
                _properties.SetContainer(_textDraw);
            }
            else if (_textDraw != null && Owner == null)
            {
                _textDraw.Click -= TextDrawOnClick;
                _textDraw = null;
                _properties.SetContainer(null);
            }
        }

        private void TextDrawOnClick(object sender, ClickPlayerTextDrawEventArgs clickPlayerTextDrawEventArgs)
        {
            OnClick(new ControlClickEventArgs(this));
        }

        #region Properties of TextDrawControl

        public TextDrawAlignment Alignment
        {
            get { return _properties.Get<TextDrawAlignment>(); }
            set
            {
                if (_properties.Set(value))
                {
                    OnPropertyChanged();
                    CheckSize();
                    Invalidate();
                }
            }
        }

        public Color BackColor
        {
            get { return _properties.Get<Color>(); }
            set
            {
                if (_properties.Set(value))
                {
                    OnPropertyChanged();
                    Invalidate();
                }
            }
        }

        public bool UseBox
        {
            get { return _properties.Get<bool>(); }
            set
            {
                if (_properties.Set(value))
                {
                    OnPropertyChanged();
                    CheckSize();
                    Invalidate();
                }
            }
        }

        public Color BoxColor
        {
            get { return _properties.Get<Color>(); }
            set
            {
                if (_properties.Set(value))
                {
                    OnPropertyChanged();
                    Invalidate();
                }
            }
        }

        public Vector2 TextSize
        {
            get { return _properties.Get<Vector2>(); }
            set
            {
                if (_properties.Set(value))
                {
                    OnPropertyChanged();
                    CheckSize();
                    Invalidate();
                }
            }
        }

        #endregion

        #region Implementation of ISelectableControl

        public bool Selectable
        {
            get { return _properties.Get<bool>(); }
            set
            {
                if (_properties.Set(value))
                {
                    OnPropertyChanged();
                    Invalidate();
                }
            }
        }
        
        #endregion

        #region Implementation of ITextControl

        public TextDrawFont Font
        {
            get { return _properties.Get<TextDrawFont>(); }
            set
            {
                if (_properties.Set(value))
                {
                    OnPropertyChanged();
                    CheckSize();
                    Invalidate();
                }
            }
        }

        public Color ForeColor
        {
            get { return _properties.Get<Color>(); }
            set
            {
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
                if (_properties.Set(value))
                {
                    OnPropertyChanged();
                    CheckSize();
                    Invalidate();
                }
            }
        }

        public int Outline
        {
            get { return _properties.Get<int>(); }
            set
            {
                if (_properties.Set(value))
                {
                    OnPropertyChanged();
                    CheckSize();
                    Invalidate();
                }
            }
        }

        public bool Proportional
        {
            get { return _properties.Get<bool>(); }
            set
            {
                if (_properties.Set(value))
                {
                    OnPropertyChanged();
                    CheckSize();
                    Invalidate();
                }
            }
        }

        public int Shadow
        {
            get { return _properties.Get<int>(); }
            set
            {
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
                if (_properties.Set(value))
                {
                    OnPropertyChanged();
                    CheckSize();
                    Invalidate();
                }
            }
        }

        #endregion

        #region Implementation of IPreviewControl

        public int PreviewModel
        {
            get { return _properties.Get<int>(); }
            set
            {
                if (_properties.Set(value))
                {
                    OnPropertyChanged();
                    Invalidate();
                }
            }
        }

        public int PreviewPrimaryColor
        {
            get { return _properties.Get<int>(); }
            set
            {
                if (_properties.Set(value))
                {
                    OnPropertyChanged();
                    Invalidate();
                }
            }
        }

        public int PreviewSecondaryColor
        {
            get { return _properties.Get<int>(); }
            set
            {
                if (_properties.Set(value))
                {
                    OnPropertyChanged();
                    Invalidate();
                }
            }
        }

        public Vector3 PreviewRotation
        {
            get { return _properties.Get<Vector3>(); }
            set
            {
                if (_properties.Set(value))
                {
                    OnPropertyChanged();
                    Invalidate();
                }
            }
        }

        public float PreviewZoom
        {
            get { return _properties.Get<float>(); }
            set
            {
                if (_properties.Set(value))
                {
                    OnPropertyChanged();
                    Invalidate();
                }
            }
        }

        #endregion

        #region Overrides of Control

        protected override void Dispose(bool disposing)
        {
            _properties.SetContainer(null);
            _textDraw?.Dispose();
        }

        protected override void OnAbsolutePositionChanged()
        {
            _properties.Set(GetAbsolutePosition(), "Position");
            Invalidate();
            base.OnAbsolutePositionChanged();
        }

        protected override void OnVisibleChanged()
        {
            Debug.Assert(_textDraw != null);

            if (Visible)
                _textDraw.Show();
            else
                _textDraw.Hide();

            base.OnVisibleChanged();
        }
        
        protected override void OnOwnerAssigned()
        {
            CheckTextDrawExistance();

            base.OnOwnerAssigned();
        }

        protected override void OnParentAssigned()
        {
            CheckTextDrawExistance();

            base.OnParentAssigned();
        }

        protected override void OnRender()
        {
            _properties.Apply();

            base.OnRender();
        }
        
        #endregion
    }
}