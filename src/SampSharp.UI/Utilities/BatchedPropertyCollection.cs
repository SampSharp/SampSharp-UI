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
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SampSharp.UI.Utilities
{
    public class BatchedPropertyCollection<TContainer>
    {
        private readonly Dictionary<string, IBatchedProperty<TContainer>> _properties = new Dictionary<string, IBatchedProperty<TContainer>>();

        public IBatchedProperty<TContainer> this[string name]
        {
            get
            {
                if (name == null) throw new ArgumentNullException(nameof(name));
                return _properties[name];
            }
            set
            {
                if (name == null) throw new ArgumentNullException(nameof(name));
                _properties[name] = value;
            }
        }

        public void Apply()
        {
            foreach (var property in _properties.Values)
                property.Apply();
        }

        public void SetContainer(TContainer container)
        {
            foreach (var property in _properties.Values)
                property.SetContainer(container);
        }

        public bool Set(object value, [CallerMemberName] string name = null)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (this[name].Set(value))
            {
//                Debug.WriteLine($"Setting {name} to {value}");
                return true;
            }
            return false;
        }

        public object Get(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return this[name].Get();
        }

        public T Get<T>([CallerMemberName] string name = null)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return (T) Get(name);
        }
    }
}