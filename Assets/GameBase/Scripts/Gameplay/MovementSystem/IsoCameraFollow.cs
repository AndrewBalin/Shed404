using UnityEngine;

namespace Movement
{
    public class IsoCameraFollow : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset = new Vector3(-10f, -8f, -10f);
        public float smoothSpeed = 7f;

        void LateUpdate()
        {
            if (target == null) return;

            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;

            transform.LookAt(target);
        }
    }
}