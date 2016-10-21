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
using SampSharp.GameMode.API;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;
using SampSharp.UI;

namespace TestMode
{
    public class GameMode : BaseMode
    {
        public class ClientMessageTraceListener : TraceListener
        {
            public override void Write(string message)
            {
                BasePlayer.SendClientMessageToAll(Color.Gray, $"[DEBUG] {message}");
            }

            public override void WriteLine(string message)
            {
                Write(message);
            }
        }
        protected override void OnInitialized(EventArgs e)
        {
            Server.ToggleDebugOutput(true);
            Debug.Listeners.Add(new ClientMessageTraceListener());

            PlayerConnected += OnPlayerConnected;

            base.OnInitialized(e);
            
            Process.Start(@"D:\games\Rockstar Games\GTA sa\samp.exe", "127.0.0.1:8192");
        }

        #region Overrides of BaseMode

        protected override void OnPlayerDisconnected(BasePlayer player, DisconnectEventArgs e)
        {
            base.OnPlayerDisconnected(player, e);

            SendRconCommand("exit");
        }

        #endregion

        private void OnPlayerConnected(object sender, EventArgs eventArgs)
        {
//            PlayerTextDraw _textDraw = new PlayerTextDraw(sender as BasePlayer);
//            _textDraw.Position = new Vector2(10, 141);
//            _textDraw.Text = "MyTextDraw";
//            _textDraw.Width = 60;
//            _textDraw.Height= 20;
////            _textDraw.TextSize = new Vector2(60, 20);
//            _textDraw.Alignment = TextDrawAlignment.Left;
//            _textDraw.BackColor = Color.Black;
//            _textDraw.Font = TextDrawFont.Normal;
//            _textDraw.LetterSize = new Vector2(0.25, 1);
//            _textDraw.ForeColor = Color.White;
//            _textDraw.Proportional = true;
//            _textDraw.Shadow = 1;
//            _textDraw.Selectable = true;
//            _textDraw.UseBox = true;
//            _textDraw.Show();
//            (sender as BasePlayer).SelectTextDraw(Color.Red);

                        var form = new TestForm(sender as BasePlayer);
                        
                        form.Show();


            //            var t = new Timer(1000, true, true);
            //            t.Tick += (o, args) =>
            //            {
            //                if (form.Visible) form.Hide();else form.Show();
            //            };
            //            ((BasePlayer) sender).Disconnected += (o, args) => t.IsRunning = false;
        }

        protected override void OnRconCommand(RconEventArgs e)
        {
            Console.WriteLine("COMMAND: " + e.Command);
            base.OnRconCommand(e);
        }

        [Command("test")]
        public static void cmd(BasePlayer player)
        {
            player.SendClientMessage("TEST!");
//            Form.Show<LoginForm>(player);
        }
    }
}