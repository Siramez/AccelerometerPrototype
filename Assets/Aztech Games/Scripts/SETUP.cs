using UnityEngine;

namespace AztechGames
{
    [ExecuteInEditMode]
    public class SETUP : MonoBehaviour
    {
        private void OnEnable()
        {
            gameObject.AddComponent<GliderSurface_Controller>();
            gameObject.AddComponent<GliderEngine_Controller>();
            gameObject.AddComponent<PhysicOnGlider>();
            if (Camera.main.GetComponent<GliderCamera_Controller>() == null)
            {
                var camScript = Camera.main.gameObject.AddComponent<GliderCamera_Controller>();
                camScript.FPSCamera = transform;
            }
            DestroyImmediate(this);
        }
    }
}