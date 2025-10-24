using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    public class NewGameButton : MonoBehaviour
    {
        public Button button;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            button.onClick.AddListener(TaskOnClick);
        }

        void TaskOnClick()
        {
            Debug.Log("New Game");
        }
    }
}
