using UnityEngine;

namespace AztechGames
{
    public class GliderEngine_Controller : MonoBehaviour
    {
        [Tooltip("Acceleration rate of the glider engine.")]
        public float acceleration = 0f;

        private float thrust = 0f;

        /// <summary>
        /// Gets or sets the thrust value, clamped between 0 and 200.
        /// </summary>
        public float Thrust
        {
            get => Mathf.Clamp(thrust, 0f, 200f);
            set => thrust = value;
        }

        /// <summary>
        /// Handles engine inputs, adjusting thrust based on user input and slat amount.
        /// </summary>
        void EngineInputs()
        {
            if (Input.GetKey(KeyCode.J))
                thrust += Time.deltaTime * acceleration;
            else if (Input.GetKey(KeyCode.K))
                thrust -= Time.deltaTime * acceleration;

            thrust -= GliderSurface_Controller.Instance.SlatAmount * Time.deltaTime;
        }

        private void Update()
        {
            if (GliderSurface_Controller.Instance != null)
            {
                GliderSurface_Controller.Instance.GetInputs();
                GliderSurface_Controller.Instance.PlaneRotations();
                EngineInputs();
            }
        }
    }
}