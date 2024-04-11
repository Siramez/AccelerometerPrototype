using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AztechGames
{
    /// <summary>
    /// Manages the user interface for the glider simulation, displaying real-time flight metrics.
    /// </summary>
    public class GliderUI_Manager : MonoBehaviour
    {
        [Header("UI Elements")]
        [Tooltip("Text element displaying airspeed.")]
        public TextMeshProUGUI Airspeed;
        [Tooltip("Text element displaying true airspeed.")]
        public TextMeshProUGUI Tas;
        [Tooltip("Text element displaying indicated airspeed.")]
        public TextMeshProUGUI Ias;
        [Tooltip("Text element displaying roll angle.")]
        public TextMeshProUGUI Roll;
        [Tooltip("Text element displaying pitch angle.")]
        public TextMeshProUGUI Pitch;
        [Tooltip("Text element displaying slat angle.")]
        public TextMeshProUGUI Slope;
        [Tooltip("Text element displaying angle of attack.")]
        public TextMeshProUGUI AOA;
        [Tooltip("Text element displaying drag force.")]
        public TextMeshProUGUI Drag;
        [Tooltip("Text element displaying lift force.")]
        public TextMeshProUGUI Lift;
        [Tooltip("Text element displaying air density.")]
        public TextMeshProUGUI Air;
        [Tooltip("Text element displaying wing area.")]
        public TextMeshProUGUI Wing;
        [Tooltip("Text element displaying temperature.")]
        public TextMeshProUGUI Temp;

        [Header("Menu Panel")]
        [Tooltip("Panel containing additional functionalities.")]
        public GameObject MenuPanel;

        private PhysicOnGlider Glider;

        private void Start()
        {
            Glider = GliderSurface_Controller.Instance.GetComponent<PhysicOnGlider>();
        }

        private void Update()
        {
            UpdateUI(Glider);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MenuPanel.SetActive(!MenuPanel.activeSelf);
            }
        }

        private void UpdateUI(PhysicOnGlider glider)
        {
            Airspeed.text = FormatText(glider.AirSpeed()) + " m/s";
            Tas.text = FormatText(glider.TrueAirSpeed()) + " m/s";
            Ias.text = FormatText(glider._rb.velocity.magnitude) + " m/s";
            Roll.text = FormatText(GliderSurface_Controller.Instance.AileronAmount) + " °";
            Pitch.text = FormatText(GliderSurface_Controller.Instance.ElevatorAmount) + " °";
            Slope.text = FormatText(GliderSurface_Controller.Instance.SlatAmount * 100f) + " °";
            Drag.text = FormatText(glider.CalculateDrag()) + " N";
            Lift.text = FormatText(glider.CalculateLift()) + " N";
            AOA.text = FormatText(Vector3.Angle(Vector3.forward, glider.transform.forward)) + " °";
            Air.text = glider.airDensity + " ρ";
            Wing.text = glider.wingArea + " m²";
            Temp.text = glider.temperature + " °C";
        }

        private string FormatText(float value)
        {
            return string.Format("{0:#.0}", value);
        }

        /// <summary>
        /// Quits the application.
        /// </summary>
        public void Quit()
        {
            Application.Quit();
        }

        /// <summary>
        /// Restarts the current scene.
        /// </summary>
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
