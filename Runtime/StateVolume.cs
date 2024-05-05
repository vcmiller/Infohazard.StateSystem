using System;
using System.Collections.Generic;
using Infohazard.Core;
using UnityEngine;

namespace Infohazard.StateSystem {
    public class StateVolume : MonoBehaviour {
        [SerializeField] private string _stateName;
        [SerializeField] private TriggerVolume _triggerVolume;

        private Dictionary<StateManager, int> _occupantActiveCount = new();

        private void Reset() {
            _triggerVolume = GetComponent<TriggerVolume>();
        }

        private void OnEnable() {
            _triggerVolume.TriggerEntered += TriggerVolume_TriggerEntered;
            _triggerVolume.TriggerExited += TriggerVolume_TriggerExited;

            foreach (GameObject occupant in _triggerVolume.Occupants) {
                TriggerVolume_TriggerEntered(occupant);
            }
        }

        private void OnDisable() {
            _triggerVolume.TriggerEntered -= TriggerVolume_TriggerEntered;
            _triggerVolume.TriggerExited -= TriggerVolume_TriggerExited;

            foreach (GameObject occupant in _triggerVolume.Occupants) {
                TriggerVolume_TriggerExited(occupant);
            }
        }

        private void TriggerVolume_TriggerEntered(GameObject other) {
            if (other.TryGetComponent(out StateManager manager)) {
                _occupantActiveCount.TryGetValue(manager, out int count);
                count++;
                _occupantActiveCount[manager] = count;
                Debug.Log($"Enabling state {_stateName} on {other.name}. New count: {count}.");

                manager.SetStateActive(_stateName, true);
            }
        }

        private void TriggerVolume_TriggerExited(GameObject other) {
            if (other.TryGetComponent(out StateManager manager)) {
                _occupantActiveCount.TryGetValue(manager, out int count);
                if (count <= 1) {
                    _occupantActiveCount.Remove(manager);
                    manager.SetStateActive(_stateName, false);
                    Debug.Log($"Disabling state {_stateName} on {other.name}.");
                } else {
                    count--;
                    _occupantActiveCount[manager] = count;
                    Debug.Log($"Decrementing state {_stateName} on {other.name}. New count: {count}.");
                }
            }
        }
    }
}
