using Pathfinding;
using UnityEngine;
using System.Collections;

namespace Units.Base
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private AIPath _aiPath;

        [SerializeField] private float _cooldown;
        
        [SerializeField] private Aggre _aggre;

        private Coroutine _currentAggroCoroutine;

        private bool _hasAggroTarget;


        private void Start()
        {
            _triggerObserver.OnTriggerEntered += TriggerEnter;
            _triggerObserver.OnTriggerExited += TriggerExit;

            SwitchFollowOff(); 
        }

        private void TriggerEnter(Collider coll)
        {
            if (!_hasAggroTarget)
            {
                _hasAggroTarget = true;
                
                TryStopRunningCoroutine();
            
                SwitchFollowOn();
            }
        }

        private void TriggerExit(Collider coll)
        {
            if (_hasAggroTarget)
            {
                _hasAggroTarget = false;
                
                _currentAggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
            }
        }

        private void TryStopRunningCoroutine()
        {
            if (_currentAggroCoroutine != null)
            {
                StopCoroutine(_currentAggroCoroutine);
                _currentAggroCoroutine = null;
            }
        }

        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return new WaitForSeconds(_cooldown);
            SwitchFollowOff();
        }

        private void SwitchFollowOn()
        {
            _aggre.enabled = true;
        }

        private void SwitchFollowOff()
        {
            _aggre.enabled = false;
        }

        private void OnDisable()
        {
            _triggerObserver.OnTriggerEntered -= TriggerEnter;
            _triggerObserver.OnTriggerExited -= TriggerExit;
        }
    }
}