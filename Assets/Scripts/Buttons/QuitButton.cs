using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    public class QuitButton : MonoBehaviour
    {
        public Button button;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            button.onClick.AddListener(TaskOnClick);
        }

        void TaskOnClick()
        {
            Debug.Log("Quit");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}