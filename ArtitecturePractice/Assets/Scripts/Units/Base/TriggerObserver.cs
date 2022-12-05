using UnityEngine;
using UnityEngine.Events;

namespace Units.Base
{
    [RequireComponent(typeof(Collider))]
    public class TriggerObserver : MonoBehaviour
    {
        public event UnityAction<Collider> OnTriggerEntered;
        public event UnityAction<Collider> OnTriggerExited;
    
        private void OnTriggerEnter(Collider coll)
        {
            OnTriggerEntered?.Invoke(coll);
        }
        private void OnTriggerExit(Collider coll)
        {
            OnTriggerExited?.Invoke(coll); 
        }
    }
}