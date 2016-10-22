using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;
using SampSharp.UI;

namespace TestMode
{
    public class TestForm : Form
    {
        private Panel panel;
        private Button button;
        private Label label;
        private TextArea textArea;
        public TestForm(BasePlayer owner) : base(owner)
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Position = new Vector2(100, 150);
            Interactable = false;

            panel = new Panel();
            panel.Position = new Vector2(200, 200);
            panel.Size = new Vector2(150, 50);

//            button = new Button();
//            button.Text = "Some Button";
//            button.Size = new Vector2(100, 40);
//            button.Click += button_Click;

            label = new Label();
            label.Position = new Vector2(0, 0);
            label.Text = "Some text here";
            label.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;

            textArea = new TextArea();
            textArea.Position = new Vector2(20, 20);
            textArea.Size = new Vector2(150, 60);
            textArea.Font = TextDrawFont.Normal;
            textArea.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quamquam id quidem licebit iis existimare, qui legerint.";


            panel.Controls.Add(label);
//            panel.Controls.Add(button);

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
