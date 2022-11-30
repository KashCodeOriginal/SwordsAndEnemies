using UnityEngine;

namespace Services.Input
{
    public abstract class InputService : IInputService
    {
        public abstract Vector2 Axis { get; }
        public bool IsAttackButtonUp() => SimpleInput.GetButtonUp(fireButton);
        
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        private const string fireButton = "Fire";

        protected Vector2 GetSimpleInputAxis() => new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}