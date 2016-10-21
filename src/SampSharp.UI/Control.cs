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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SampSharp.GameMode;
using SampSharp.GameMode.Tools;
using SampSharp.GameMode.World;

namespace SampSharp.UI
{
    /// <summary>
    ///     Represents a visible user interface component.
    /// </summary>
    public abstract class Control : Disposable, INotifyPropertyChanged
    {
        private bool _isInvalidated;
        private bool _isPerformingLayout;
        private int _layoutSuspendCount;
        private Vector2 _position;
        private Vector2 _reportedAbsolutePosition;
        private bool _reportedIsVisible;
        private Vector2 _size;
        private bool _visible = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="Control"/> class.
        /// </summary>
        protected Control()
        {
            _reportedIsVisible = Visible;
        }

        #region Implementation of INotifyPropertyChanged

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Overrides of Disposable

        protected override void Dispose(bool disposing)
        {
        }

        #endregion

        #region Properties of Control

        /// <summary>
        ///     Gets the parent controls.
        /// </summary>
        public Control Parent { get; private set; }

        /// <summary>
        ///     Gets the owner of this instance.
        /// </summary>
        public virtual BasePlayer Owner => Parent?.Owner;

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        public virtual Vector2 Position
        {
            get { return _position; }
            set
            {
                AssertNotDisposed();

                if (value != _position)
                {
                    _position = value;

                    OnPositionChanged();
                    OnPropertyChanged();
                    CheckAbsolutePosition();
                    Invalidate();
                }
            }
        }

        /// <summary>
        ///     Gets a value indicating whether this <see cref="Control" /> is visible.
        /// </summary>
        public virtual bool Visible => _visible && (Parent?.Visible ?? false);

        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        public virtual Vector2 Size
        {
            get { return _size; }
            set
            {
                AssertNotDisposed();

                if (value != _size)
                {
                    _size = value;

                    OnSizeChanged();
                    OnPropertyChanged();
                    Invalidate();
                }
            }
        }

        /// <summary>
        ///     Gets or sets position relative to the left of the parent control.
        /// </summary>
        public float Left
        {
            get { return Position.X; }
            set { Position = new Vector2(value, Position.Y); }
        }

        /// <summary>
        ///     Gets or sets position relative to the top of the parent control.
        /// </summary>
        public float Top
        {
            get { return Position.Y; }
            set { Position = new Vector2(Position.X, value); }
        }

        /// <summary>
        ///     Gets the width.
        /// </summary>
        public float Width => Size.X;

        /// <summary>
        ///     Gets the height.
        /// </summary>
        public float Height => Size.X;

        #endregion

        #region Events of Control

        /// <summary>
        ///     Occurs when the position has been changed.
        /// </summary>
        public event EventHandler PositionChanged;

        /// <summary>
        ///     Occurs when the size has been changed.
        /// </summary>
        public event EventHandler SizeChanged;

        /// <summary>
        ///     Occurs when this instance is being rendered.
        /// </summary>
        public event EventHandler Render;

        /// <summary>
        ///     Occurs when the visibility value has changed.
        /// </summary>
        public event EventHandler VisibleChanged;

        /// <summary>
        ///     Occurs when a parent <see cref="Control" /> has been assigned.
        /// </summary>
        public event EventHandler ParentAssigned;

        /// <summary>
        ///     Occurs when an owner has been assigned.
        /// </summary>
        public event EventHandler OwnerAssigned;

        /// <summary>
        ///     Occurs when the absolute position of this <see cref="Control" /> changed.
        /// </summary>
        public event EventHandler AbsolutePositionChanged;

        /// <summary>
        ///     Occurs when this <see cref="Control" /> has been clicked by the user.
        /// </summary>
        public event EventHandler<ControlClickEventArgs> Click;

        #endregion

        #region Public methods of Control

        /// <summary>
        ///     Requests this <see cref="Control" /> to be redrawn.
        /// </summary>
        public virtual void Invalidate()
        {
            AssertNotDisposed();

            _isInvalidated = true;

            if ((_layoutSuspendCount == 0) && !_isPerformingLayout)
                PerformLayout();
        }

        /// <summary>
        ///     Prevents future calls to <see cref="Invalidate" /> to cause this <see cref="Control" /> to be redrawn. To resume
        ///     normal layout, call <see cref="ResumeLayout()" />.
        /// </summary>
        public void SuspendLayout()
        {
            AssertNotDisposed();

            _layoutSuspendCount++;
        }

        /// <summary>
        ///     Resumes the layout as usual. If this <see cref="Control" /> has been invalidated since the layout has been
        ///     suspended, it will be redrawn.
        /// </summary>
        public void ResumeLayout()
        {
            AssertNotDisposed();

            ResumeLayout(true);
        }

        /// <summary>
        ///     Resumes the layout as usual.
        /// </summary>
        /// <param name="performLayout">
        ///     If set to <c>true</c> and this <see cref="Control" /> has been invalidated since the layout
        ///     has been suspended, it will be redrawn.
        /// </param>
        public void ResumeLayout(bool performLayout)
        {
            AssertNotDisposed();

            if (_layoutSuspendCount > 0)
            {
                _layoutSuspendCount--;

                if (_isInvalidated && performLayout)
                    Invalidate();
            }
        }

        /// <summary>
        ///     Shows this instance.
        /// </summary>
        public virtual void Show()
        {
            AssertNotDisposed();

            _visible = true;
            CheckVisiblity();
        }

        /// <summary>
        ///     Hides this instance.
        /// </summary>
        public virtual void Hide()
        {
            AssertNotDisposed();

            _visible = false;
            CheckVisiblity();
        }

        /// <summary>
        ///     Assigns the parent <see cref="Control" />.
        /// </summary>
        /// <param name="value">The parent <see cref="Control" />.</param>
        public void AssignParent(Control value)
        {
            AssertNotDisposed();

            var owner = Owner;

            if (Parent != null)
            {
                Parent.OwnerAssigned -= ParentOnOwnerAssigned;
                Parent.VisibleChanged -= ParentOnVisibleChanged;
                Parent.AbsolutePositionChanged -= ParentOnAbsolutePositionChanged;
            }

            Parent = value;

            if (value != null)
            {
                value.OwnerAssigned += ParentOnOwnerAssigned;
                value.VisibleChanged += ParentOnVisibleChanged;
                value.AbsolutePositionChanged += ParentOnAbsolutePositionChanged;
            }

            OnParentAssigned();
            OnPropertyChanged("Parent");

            CheckAbsolutePosition();

            var newOwner = Owner;

            if (owner != newOwner)
                OnOwnerAssigned();
        }

        /// <summary>
        ///     Gets the absolute position.
        /// </summary>
        public virtual Vector2 GetAbsolutePosition()
        {
            var parentPosition = Parent?.GetAbsolutePosition() ?? Vector2.Zero;

            return parentPosition + Position;
        }

        #endregion

        #region Protected methods of Control

        /// <summary>
        ///     Performs the layout.
        /// </summary>
        protected void PerformLayout()
        {
            AssertNotDisposed();

            if (_isInvalidated && (_layoutSuspendCount == 0) && (Owner != null))
            {
                _isPerformingLayout = true;

                OnRender();

                _isInvalidated = false;
                _isPerformingLayout = false;
            }
        }

        #endregion

        #region Private methods of Control

        /// <summary>
        ///     Checks whether the absolute position has changed.
        /// </summary>
        private void CheckAbsolutePosition()
        {
            var value = GetAbsolutePosition();

            if (value != _reportedAbsolutePosition)
            {
                _reportedAbsolutePosition = value;
                OnAbsolutePositionChanged();
                Invalidate();
            }
        }

        /// <summary>
        ///     Checks whether the visiblity has changed.
        /// </summary>
        private void CheckVisiblity()
        {
            var value = Visible;

            if (value != _reportedIsVisible)
            {
                _reportedIsVisible = value;
                OnVisibleChanged();
                Invalidate();
            }
        }

        private void ParentOnVisibleChanged(object sender, EventArgs eventArgs)
        {
            CheckVisiblity();
        }

        private void ParentOnAbsolutePositionChanged(object sender, EventArgs eventArgs)
        {
            CheckAbsolutePosition();
        }

        private void ParentOnOwnerAssigned(object sender, EventArgs eventArgs)
        {
            OnOwnerAssigned();
        }

        #endregion

        #region Event invocators of Control

        /// <summary>
        ///     Raises the <see cref="E:AbsolutePositionChanged" /> event.
        /// </summary>
        protected virtual void OnAbsolutePositionChanged()
        {
            AbsolutePositionChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Raises the <see cref="E:VisibleChanged" /> event.
        /// </summary>
        protected virtual void OnVisibleChanged()
        {
            VisibleChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Raises the <see cref="E:PositionChanged" /> event.
        /// </summary>
        protected virtual void OnPositionChanged()
        {
            PositionChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Raises the <see cref="E:SizeChanged" /> event.
        /// </summary>
        protected virtual void OnSizeChanged()
        {
            SizeChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Raises the <see cref="E:Render" /> event.
        /// </summary>
        protected virtual void OnRender()
        {
            Render?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Raises the <see cref="E:PropertyChanged" /> event.
        /// </summary>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Raises the <see cref="E:ParentAssigned" /> event.
        /// </summary>
        protected virtual void OnParentAssigned()
        {
            ParentAssigned?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Raises the <see cref="E:OwnerAssigned" /> event.
        /// </summary>
        protected virtual void OnOwnerAssigned()
        {
            OwnerAssigned?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Raises the <see cref="E:Click" /> event.
        /// </summary>
        protected virtual void OnClick(ControlClickEventArgs args)
        {
            Click?.Invoke(this, args);

            if (!args.PreventPropagation)
                Parent?.OnClick(args);
        }

        #endregion
    }

    public class ControlClickEventArgs : EventArgs
    {
        public ControlClickEventArgs(Control clickedControl)
        {
            if (clickedControl == null) throw new ArgumentNullException(nameof(clickedControl));
            ClickedControl = clickedControl;
        }

        public Control ClickedControl { get; }
        public bool PreventPropagation { get; set; }
    }
}