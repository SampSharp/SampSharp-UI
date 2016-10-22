using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampSharp.GameMode;
using SampSharp.GameMode.Helpers;
using SampSharp.GameMode.SAMP;

namespace SampSharp.UI
{
    public class ProgressBar : Control
    {
        private Panel _container;
        private Panel _fill;
        private Panel _main;

        private Color _color = Color.Gray;
        private float _minValue;
        private float _maxValue = 1;
        private float _value;

        public ProgressBar()
        {
            InitializeComponent();
        }

        public Color Color
        {
            get { return _color; }
            set
            {
                AssertNotDisposed();

                if (_color == value)
                    return;

                _color = Color;
                
                OnPropertyChanged();
                Invalidate();
            }
        }

        public float MinValue
        {
            get { return _minValue; }
            set
            {
                AssertNotDisposed();

                if (_minValue.Equals(value))
                    return;

                _minValue = value;

                SuspendLayout();
                _maxValue = Math.Max(_minValue, _maxValue);
                Value = Math.Max(Value, _minValue);

                OnPropertyChanged();
                Invalidate();
                ResumeLayout();
            }
        }

        public float MaxValue
        {
            get { return _maxValue; }
            set
            {
                AssertNotDisposed();

                if (_maxValue.Equals(value))
                    return;

                _maxValue = value;

                SuspendLayout();
                _minValue = Math.Min(_minValue, _maxValue);
                Value = Math.Max(Value, _minValue);

                OnPropertyChanged();
                Invalidate();
                ResumeLayout();
            }
        }

        public float Value
        {
            get { return _value; }
            set
            {
                AssertNotDisposed();

                if (_value.Equals(value))
                    return;

                _value = value; 
                
                OnPropertyChanged();
                Invalidate();
            }
        }

        public float NormalizedValue
        {
            get { return (Value - MinValue)/(MaxValue - MinValue); }
            set { Value = MathHelper.Lerp(MinValue, MaxValue, value); }
        }

        private void InitializeComponent()
        {
            _container = new Panel();
            _fill = new Panel();
            _main = new Panel();

            _container.AssignParent(this);
            _fill.AssignParent(_container);
            _main.AssignParent(_fill);

            _container.Controls.Add(_fill);
        }

        #region Overrides of Control
        
        protected override bool IgnorePadding => true;

        protected override void OnRender()
        {
            var size = Size - Padding.Size;

            _container.SuspendLayout();
            _container.BoxColor = new Color((byte) 0, (byte) 0, (byte) 0, Color.A);
            _container.Padding = Padding;
            _container.Size = Size;
            _container.ResumeLayout();

            _fill.SuspendLayout();
            _fill.BoxColor = new Color(Color.R, Color.G, Color.B, (byte) (0x66 & (Color.A/2)));
            _fill.Size = size;
            _fill.ResumeLayout();

            _main.SuspendLayout();
            _main.BoxColor = Color;
            _main.Size = new Vector2(size.X/2, size.Y);
            _main.ResumeLayout();

            base.OnRender();
        }

        #endregion
    }
}
