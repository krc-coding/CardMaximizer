using UnityEngine;
using TMPro;
using System.Text;

public class GameUI : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private State state;
    [SerializeField] private RuleManager ruleManager;
    [SerializeField] private GameManager gameManager;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI rulesText;
    [SerializeField] private TextMeshProUGUI gameStateText;
    [SerializeField] private TextMeshProUGUI turnInfoText;

    [Header("Rules Popup")] 
    [SerializeField] private GameObject rulesPopup;
    [SerializeField] private bool startWithPopupClosed = true;

    void Start()
    {
        if (state == null) state = FindObjectOfType<State>();
        if (ruleManager == null) ruleManager = FindObjectOfType<RuleManager>();
        if (gameManager == null) gameManager = FindObjectOfType<GameManager>();

        // Initialize popup state
        if (rulesPopup != null)
        {
            rulesPopup.SetActive(!startWithPopupClosed);
        }

        UpdateRulesDisplay();
    }

    void Update()
    {
        // Only update rules if popup is visible
        if (rulesPopup != null && rulesPopup.activeSelf)
        {
            UpdateRulesDisplay();
        }

        UpdateGameStateDisplay();
        UpdateTurnDisplay();
    }

    public void ToggleRulesPopup()
    {
        if (rulesPopup != null)
        {
            bool newState = !rulesPopup.activeSelf;
            rulesPopup.SetActive(newState);

            // Update the display when opening
            if (newState)
            {
                UpdateRulesDisplay();
            }
        }
    }

    public void ShowRulesPopup()
    {
        if (rulesPopup != null)
        {
            rulesPopup.SetActive(true);
            UpdateRulesDisplay();
        }
    }

    public void HideRulesPopup()
    {
        if (rulesPopup != null)
        {
            rulesPopup.SetActive(false);
        }
    }

    private void UpdateRulesDisplay()
    {
        if (rulesText == null || ruleManager == null) return;

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("=== ACTIVE RULES ===");

        var setupRule = ruleManager.GetSetupRule();
        if (setupRule != null)
        {
            sb.AppendLine($"\n<b>Rules:</b>");
            foreach (var rule in setupRule.rulesList)
            {
                sb.AppendLine($"• {rule}");
            }
        }

        var playRules = ruleManager.GetPlayRules();
        if (playRules.Count > 0)
        {
            foreach (var rule in playRules)
            {
                sb.AppendLine($"• {rule.RuleName}");
            }
        }

        rulesText.text = sb.ToString();
    }

    private void UpdateGameStateDisplay()
    {
        if (gameStateText == null || state == null) return;

        StringBuilder sb = new StringBuilder();

        if (state.winConditionType == State.WinConditionType.LastManStanding)
        {
            sb.AppendLine($"<b>Your Life:</b> {state.playerLife}");
            sb.AppendLine($"<b>Opponent Life:</b> {state.opponentLife}");
        }
        else
        {
            sb.AppendLine($"<b>Your Points:</b> {state.playerPoints}/{state.pointsToWin}");
            sb.AppendLine($"<b>Opponent Points:</b> {state.opponentPoints}/{state.pointsToWin}");
        }

        sb.AppendLine($"\n<b>Hand:</b> {state.playerCards.Count}/{state.maxHandSize}");
        sb.AppendLine($"<b>Actions taken:</b> {state.actionsThisTurn}/{state.maxActionsPerTurn}");

        gameStateText.text = sb.ToString();
    }

    private void UpdateTurnDisplay()
    {
        if (turnInfoText == null || state == null) return;

        StringBuilder sb = new StringBuilder();

        if (gameManager.isPlayerTurn)
        {
            sb.AppendLine($"<b>It's your turn</b>");
        }
        else
        {
            sb.AppendLine($"<b>It's the opponent's turn</b>");
        }

        turnInfoText.text = sb.ToString();
    }
}