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

namespace SampSharp.UI.Utilities
{
    public class BatchedProperty<TContainer,TValue> : IBatchedProperty<TContainer>
    {
        private readonly Action<TContainer, TValue> _apply;
        private bool _anySet;
        private bool _hasChanged;
        private TContainer _container;
        private TValue _value;

        public BatchedProperty(Action<TContainer, TValue> apply)
        {
            if (apply == null) throw new ArgumentNullException(nameof(apply));
            _apply = apply;
        }

        public BatchedProperty(Action<TContainer, TValue> apply, TValue defaultValue) : this(apply)
        {
            _value = defaultValue;
        }

        object IBatchedProperty<TContainer>.Get() => Get();
        bool IBatchedProperty<TContainer>.Set(object value) => Set((TValue) value);

        public TValue Get() => _value;

        public bool Set(TValue value)
        {
            if (Equals(_value, value) && _anySet)
                return false;

            _hasChanged = true;
            _anySet = true;
            _value = value;

            return true;
        }

        public void SetContainer(TContainer container)
        {
            _container = container;
            _hasChanged = true;
            Apply();
        }

        public void Apply()
        {
            if (_hasChanged && _anySet && _container != null)
            {
                _apply(_container, _value);
                _hasChanged = false;
            }
        }
    }
}