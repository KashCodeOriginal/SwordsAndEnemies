using Units.Base;
using UnityEngine;

namespace Units.Enemy.Logic
{
    public class RotateToPlayer : Aggre
    {
        [SerializeField] private float _speed;

        private Transform _playerTransform;

        private Vector3 _positionToLook;

        private void Update()
        {
            RotateHeroTowards();
        }

        public void SetPlayer(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        private void RotateHeroTowards()
        {
            UpdateLookAtPosition();

            transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
        }

        private void UpdateLookAtPosition()
        {
            var enemyPosition = transform.position;

            var positionDifference = _playerTransform.position - enemyPosition;

            _positionToLook = new Vector3(positionDifference.x, enemyPosition.y, positionDifference.z);
        }

        private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook)
        {
            return Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());
        }

        private float SpeedFactor()
        {
            return _speed * Time.deltaTime;
        }

        private Quaternion TargetRotation(Vector3 position)
        {
            return Quaternion.LookRotation(position);
        }
    }
}
