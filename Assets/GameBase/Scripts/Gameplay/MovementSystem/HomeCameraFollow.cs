using UnityEngine;

namespace Movement
{
    public class HomeCameraFollow: MonoBehaviour
    {
        public Transform target;
        public float height = 20f;
        public float smoothSpeed = 5f;

        void LateUpdate()
        {
            if (!target) return;

            Vector3 desiredPosition = target.position + Vector3.up * height;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            transform.rotation = Quaternion.Euler(90f, 90f, 0f);
        }
    }
}