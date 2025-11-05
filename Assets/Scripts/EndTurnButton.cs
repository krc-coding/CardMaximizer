using UnityEngine;
using UnityEngine.UI;

public class EndTurnButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;
    
    void Start()
    {
        button = GetComponent<Button>();
        gameManager = FindObjectOfType<GameManager>();
        
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
    }
    
    private void OnClick()
    {
        if (gameManager != null)
        {
            gameManager.EndTurn();
        }
    }
}
