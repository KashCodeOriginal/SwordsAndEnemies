using UnityEngine;

namespace Services.Input
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis
        {
            get
            {
                var axis = GetSimpleInputAxis();

                if (axis == Vector2.zero)
                {
                    axis = GetUnityAxis();
                }
                    
                return axis;
            }
        }
        private Vector2 GetUnityAxis() => new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
    }
}