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
using SampSharp.GameMode.World;
using SampSharp.UI;

namespace TestMode
{
    public class TestForm : Form
    {
        private Button button;
        private Label label;
        private Label label2;
        private Panel panel;
        private TextArea textArea;
        private ProgressBar progressBar;

        public TestForm(BasePlayer owner) : base(owner)
        {
            Interactable = true;

            Button b = new Button();
            b.Size = new Vector2(20, 20);
            b.Position = new Vector2(50, Screen.Size.Y - 60);
            Debug.WriteLine("b pos" + b.Position);
            b.Click += (sender, args) =>
            {
                b.Position += new Vector2(0,1);
                b.Owner.SendClientMessage("pos " + (b.Position.Y - (Screen.Size.Y - 20)));
            };
            b.Text = "X";
            Controls.Add(b);

            for (int i = 0; i < 10; i++)
            {
                Panel p = new Panel();
                p.Size = new Vector2(10);
                p.Position = new Vector2(i*10);
                Controls.Add(p);
            }
            //InitializeComponent();
        }

        private void InitializeComponent()
        {
            //
            // TestForm
            //
            Position = new Vector2(100, 150);
            Interactable = false;
            //
            // panel
            //
            panel = new Panel();
            panel.Position = new Vector2(200, 200);
            panel.Size = new Vector2(150, 50);
            //
            // button
            //
//            button = new Button();
//            button.Text = "Some Button";
//            button.Size = new Vector2(100, 40);
//            button.Click += button_Click;
            //
            // label
            //
            label = new Label();
            label.Position = new Vector2(0, 0);
            label.Text = "Bottom right I";
            label.Shadow = 0;
            label.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            //
            // label2
            //
            label2 = new Label();
            label2.Position = new Vector2(0, 0);
            label2.Text = "I bottom left";
            label2.Shadow = 0;
            label2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            label2.Margin = new Padding(5);
            //
            // textArea
            //
            textArea = new TextArea();
            textArea.Position = new Vector2(20, 20);
            textArea.Size = new Vector2(150, 60);
            textArea.Font = TextDrawFont.Normal;
            textArea.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quamquam id quidem licebit iis existimare, qui legerint.";
            //
            // progressBar
            //
            progressBar = new ProgressBar();
            progressBar.Position = new Vector2(30, 0);
            progressBar.Size = new Vector2(150, 19);
//            progressBar.Padding = new Padding(5);
            progressBar.Value = 0.25f;

            panel.Controls.Add(label);
            panel.Controls.Add(label2);
//            panel.Controls.Add(button);

            Controls.Add(progressBar);
            Controls.Add(textArea);
//            Controls.Add(label);
//            Controls.Add(button);
            Controls.Add(panel);
        }

        private void button_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("CLICK!!!");
        }
    }
}