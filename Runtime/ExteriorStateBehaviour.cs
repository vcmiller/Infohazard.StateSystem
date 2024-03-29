﻿// The MIT License (MIT)
// 
// Copyright (c) 2022-present Vincent Miller
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Infohazard.StateSystem {
    public class ExteriorStateBehaviour : MonoBehaviour, IStateBehaviour {
        [SerializeField, FormerlySerializedAs("component")]
        protected Component _component;
        [SerializeField, FormerlySerializedAs("states")]
        protected StateList _states;

        private bool _initialized = false;

        public Type Type => _component ? _component.GetType() : typeof(Component);

        protected virtual void Awake() {
            Initialize();
        }

        private void Initialize() {
            if (_initialized) return;
            _initialized = true;
            _states.Initialize(_component);
        }

        public bool HasState(string stateName) => _states.HasState(stateName);
        public bool IsStateActive(string stateName) => _states.IsStateActive(stateName);
        public void SetStateActive(string stateName, bool value) {
            Initialize();
            _states.SetStateActive(stateName, value);
        }
    }

}