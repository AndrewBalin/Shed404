using UnityEngine;

namespace Movement
{
    [ExecuteAlways]
    public class FramedCameraFollow : MonoBehaviour
    {
        public Transform target;

        [Header("Framing Box (world space)")] 
        public Vector2 frameSize = new Vector2(5f, 3f);
        public float frameZOffset = 1.5f;

        [Header("Camera Motion")] 
        public float smoothSpeed = 5f;
        public bool fixCameraRotation = false;
        
        [Header("Camera Offset")] 
        public float height = 10f;
        public float distance = 10f;
        public float angle = 45f;
        
        [Header("Camera LookAt Offset")]
        public Vector3 lookAtOffset = new Vector3(0, 1.5f, 0);
        
        
        private Vector3 frameCenter;
        private Vector3 velocity;

        void Start()
        {
            if (target != null)
                frameCenter = target.position;
        }

        void LateUpdate()
        {
            if (target == null) return;
            
            Quaternion rot = Quaternion.Euler(angle, 0f, 0f);
            Vector3 cameraOffset = rot * Vector3.back * distance + Vector3.up * height;
            Vector3 desiredPos = frameCenter + cameraOffset;

            transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
            Vector3 frameOffset = Vector3.zero;

            if (fixCameraRotation)
            {
                Vector3 forwardXZ = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
                frameOffset = forwardXZ * frameZOffset;
            }
            else
            {
                frameOffset = new Vector3(0f, 0f, frameZOffset);
            }
            
            Vector3 targetWithOffset = target.position + frameOffset;
            Vector3 delta = new Vector3(targetWithOffset.x - frameCenter.x, 0f, targetWithOffset.z - frameCenter.z);

            if (Mathf.Abs(delta.x) > frameSize.x / 2)
                frameCenter.x = targetWithOffset.x - Mathf.Sign(delta.x) * (frameSize.x / 2);

            if (Mathf.Abs(delta.z) > frameSize.y / 2)
                frameCenter.z = targetWithOffset.z - Mathf.Sign(delta.z) * (frameSize.y / 2);
        }

        void OnDrawGizmos()
        {
            if (!Application.isPlaying || target == null) return;

            Vector3 visualCenter = frameCenter;
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(new Vector3(visualCenter.x, target.position.y, visualCenter.z),
                new Vector3(frameSize.x, 0f, frameSize.y));
        }
    }
}