using UnityEngine;

namespace AztechGames
{ 
    [ExecuteInEditMode]
    public class GliderCamera_Controller : MonoBehaviour
    { 
        [Tooltip("The mode of the camera (First Person or Third Person).")]
        public enum CameraMode
        {
            FPS,
            TPS
        }
        public CameraMode cameraMode;
        
        [Space(10)]
        [Tooltip("The transform of the First Person Camera.")]
        public Transform FPSCamera;
        
        [Space(5)]
        [Tooltip("The allowed range for horizontal rotation of the camera.")]
        public Vector2 minMaxXAngle = new Vector2(-70.0f, 70.0f);
        [Tooltip("The allowed range for vertical rotation of the camera.")]
        public Vector2 minMaxYAngle = new Vector2(-180.0f, 180.0f);
        [Tooltip("The allowed range for zoom distance.")]
        public Vector2 minMaxZoomDistance = new Vector2(5.0f, 50.0f);
        [Tooltip("The offset to look at when in Third Person mode.")]
        public Vector3 lookAtOffset;
        
        [Space(5)]
        [Tooltip("The speed of camera rotation.")]
        public float rotationSpeed = 5.0f;
        [Tooltip("The speed of camera zooming.")]
        public float zoomSpeed = 25.0f;

        private Transform Target;
        private float currentX = 0.0f; 
        private float currentY = 0.0f;
        
        void Start() 
        { 
            Target = GliderSurface_Controller.Instance.transform;
            transform.parent = FPSCamera;
        } 
        
        void LateUpdate() 
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                cameraMode = CameraMode.TPS;
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                cameraMode = CameraMode.FPS;
            }

            switch (cameraMode)
            {
                case CameraMode.FPS:transform.position = FPSCamera.position; transform.rotation = FPSCamera.rotation;
                    break;
                case CameraMode.TPS: CameraMovement();
                    break;
            }
        }

        void CameraMovement()
        {
            float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed;
            float verticalInput = Input.GetAxis("Mouse Y") * rotationSpeed;
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
                
            currentX = Mathf.Clamp(currentX + horizontalInput, minMaxYAngle.x, minMaxYAngle.y);;
            currentY = Mathf.Clamp(currentY - verticalInput, minMaxXAngle.x, minMaxXAngle.y);
        
            transform.RotateAround(Target.position, Vector3.up, horizontalInput);
            transform.RotateAround(Target.position, Vector3.right, verticalInput);
        
            float distance = Mathf.Clamp(Vector3.Distance(transform.position, Target.position), minMaxZoomDistance.x, minMaxZoomDistance.y);
            distance -= scrollInput * zoomSpeed;
            distance = Mathf.Clamp(distance, minMaxZoomDistance.x, minMaxZoomDistance.y);
                
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            Vector3 offset = rotation * new Vector3(0, 0, -distance);
        
            transform.position = Target.position + offset;

            transform.LookAt(Target.position + lookAtOffset);
        }
    }
}
