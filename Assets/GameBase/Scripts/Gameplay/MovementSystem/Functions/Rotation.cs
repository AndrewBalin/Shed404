using UnityEngine;

namespace GameBase.Scripts.Gameplay.MovementSystem.Functions
{
    public class Rotation
    {
        private readonly float _turnSpeed;

        public Rotation(float turnSpeed)
        {
            _turnSpeed = turnSpeed;
        }

        public void UpdateRotation(Transform transform, Vector3 velocity)
        {
            if (velocity.sqrMagnitude < 0.01f)
                return;
            
            Quaternion target = Quaternion.LookRotation(velocity.normalized);
            
            float maxDegreesDelta = _turnSpeed * Time.deltaTime;
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, maxDegreesDelta);
        }
    }
}