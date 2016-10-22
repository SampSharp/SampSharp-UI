using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;
using SampSharp.UI.Utilities;

namespace SampSharp.UI
{
    public class TextArea : Panel, ITextControl
    {
        private Label _label;
        private string _text;

        private readonly BatchedPropertyCollection<Label> _properties = new BatchedPropertyCollection<Label>
        {
            ["Font"] = new BatchedProperty<Label, TextDrawFont>((t, v) => t.Font = v),
            ["ForeColor"] = new BatchedProperty<Label, Color>((t, v) => t.ForeColor = v),
            ["LetterSize"] = new BatchedProperty<Label, Vector2>((t, v) => t.LetterSize = v),
            ["Outline"] = new BatchedProperty<Label, int>((t, v) => t.Outline = v),
            ["Proportional"] = new BatchedProperty<Label, bool>((t, v) => t.Proportional = v, true),
            ["Shadow"] = new BatchedProperty<Label, int>((t, v) => t.Shadow = v),
            ["Text"] = new BatchedProperty<Label, string>((t, v) => t.Text = v),
        };

        public TextArea()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            Shadow = 0;
            LetterSize = new Vector2(0.18f, 0.9f);

            _label = new Label();

            Controls.Add(_label);
            
            _properties.SetContainer(_label);
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
                    CheckTextSize();
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
                    CheckTextSize();
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
                    CheckTextSize();
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
            get { return _text; }
            set
            {
                AssertNotDisposed();
                
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged();
                }

                if (_properties.Set(MakeFitting(value)))
                {
                    Invalidate();
                }
            }
        }

        #endregion

        private string MakeFitting(string value)
        {
            return ControlHelper.FitTextInWidth(value, Font, LetterSize, Proportional, Size.X);
        }

        private void CheckTextSize()
        {
            _properties.Set(MakeFitting(_text), "Text");
        }
        
        #region Overrides of Control

        protected override void OnSizeChanged()
        {
            base.OnSizeChanged();

            CheckTextSize();
        }

        #endregion

        #region Overrides of Panel

        protected override void OnRender()
        {
            _label.SuspendLayout();
            _properties.Apply();
            _label.ResumeLayout();

            base.OnRender();
        }

        #endregion
    }
}
