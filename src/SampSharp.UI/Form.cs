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

using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;

namespace SampSharp.UI
{
    public class Form : ContainerControl
    {
        private bool _visible;

        public Form(BasePlayer owner)
        {
            Owner = owner;
            AssignParent(null);
        }

        #region Overrides of Control

        public override BasePlayer Owner { get; }

        public override bool Visible => _visible;

        public virtual bool Interactable { get; set; }
        public virtual Color InteractableColor { get; set; } = Color.Red;

        public override void Show()
        {
            AssertNotDisposed();

            _visible = true;
            if (Interactable)
            {
                // TODO: Stacking of selecting value
                Owner.SelectTextDraw(InteractableColor);
            }

            base.Show();
        }

        public override void Hide()
        {
            AssertNotDisposed();

            if (Interactable)
            {
                // TODO: Stacking of selecting value
                Owner.CancelSelectTextDraw();
            }

            _visible = false;
            base.Hide();
        }
        
        #endregion
    }
}