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
using SampSharp.GameMode.Events;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;

namespace TestMode
{
    public class GameMode : BaseMode
    {
        private void OnPlayerConnected(object sender, EventArgs eventArgs)
        {
            var form = new TestForm(sender as BasePlayer);

            form.Show();
        }

        #region Overrides of BaseMode

        protected override void OnInitialized(EventArgs e)
        {
            Server.ToggleDebugOutput(true);
            Debug.Listeners.Add(new ClientMessageTraceListener());

            PlayerConnected += OnPlayerConnected;

            base.OnInitialized(e);

            Process.Start(@"D:\games\Rockstar Games\GTA sa\samp.exe", "127.0.0.1:8192");
        }

        protected override void OnPlayerDisconnected(BasePlayer player, DisconnectEventArgs e)
        {
            base.OnPlayerDisconnected(player, e);

            SendRconCommand("exit");
        }

        #endregion
    }
}