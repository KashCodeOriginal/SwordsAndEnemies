using Enemy.Logic;
using Pathfinding;
using UnityEngine;

namespace Units.Base
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private MoveToPlayer _follow;
        [SerializeField] private AIPath _aiPath;

        private void Start()
        {
            _triggerObserver.OnTriggerEntered += TriggerEnter;
            _triggerObserver.OnTriggerExited += TriggerExit;

            SwitchFollowOff(); 
        }

        private void TriggerEnter(Collider coll)
        {
            SwitchFollowOn();
        }

        private void TriggerExit(Collider coll)
        {
            SwitchFollowOff();
        }

        private void SwitchFollowOn()
        {
            _follow.enabled = true;
            _aiPath.enabled = true;
        }

        private void SwitchFollowOff()
        {
            _follow.enabled = false;
            _aiPath.enabled = false;
        }

        private void OnDisable()
        {
            _triggerObserver.OnTriggerEntered -= TriggerEnter;
            _triggerObserver.OnTriggerExited -= TriggerExit;
        }
    }
}