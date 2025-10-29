using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Rules;

public class RuleManager : MonoBehaviour
{
    [Header("Setup Rule Configuration")]
    [Tooltip("If true, randomly selects a setup rule. If false, uses the first available.")]
    [SerializeField]
    private bool randomizeSetupRule = true;

    [Header("Play Rules Configuration")] [SerializeField]
    private int minPlayRules = 0;

    [SerializeField] private int maxPlayRules = 3;

    [Header("References")] [SerializeField]
    private State gameState;

    private ISetupRule activeSetupRule;
    private List<IPlayRule> activePlayRules = new List<IPlayRule>();

    private List<System.Type> availableSetupRuleTypes = new List<System.Type>();
    private List<System.Type> availablePlayRuleTypes = new List<System.Type>();

    void Awake()
    {
        if (gameState == null)
        {
            gameState = FindObjectOfType<State>();
        }

        // Find all rule types in the assembly
        DiscoverRules();
    }

    void Start()
    {
        GenerateRuleSet();
    }

    /// <summary>
    /// Discovers all classes that implement ISetupRule and IPlayRule
    /// </summary>
    private void DiscoverRules()
    {
        availableSetupRuleTypes.Clear();
        availablePlayRuleTypes.Clear();

        var assembly = typeof(IRule).Assembly;

        // Find all setup rules
        availableSetupRuleTypes = assembly.GetTypes()
            .Where(t => typeof(ISetupRule).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToList();

        // Find all play rules
        availablePlayRuleTypes = assembly.GetTypes()
            .Where(t => typeof(IPlayRule).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToList();

        Debug.Log($"Discovered {availableSetupRuleTypes.Count} setup rule types");
        Debug.Log($"Discovered {availablePlayRuleTypes.Count} play rule types");
    }

    /// <summary>
    /// Generates a random set of rules for the game
    /// </summary>
    public void GenerateRuleSet()
    {
        // Clean up existing rules
        ClearRules();

        // Reset game state to defaults
        if (gameState != null)
        {
            gameState.ResetGameState();
        }

        // Select ONE setup rule
        SelectSetupRule();

        // Select multiple play rules
        SelectPlayRules();

        LogActiveRules();
    }

    /// <summary>
    /// Selects and initializes a single setup rule
    /// </summary>
    private void SelectSetupRule()
    {
        if (availableSetupRuleTypes.Count == 0)
        {
            Debug.LogWarning("No setup rules available!");
            return;
        }

        System.Type selectedType;

        if (randomizeSetupRule)
        {
            selectedType = availableSetupRuleTypes[Random.Range(0, availableSetupRuleTypes.Count)];
        }
        else
        {
            selectedType = availableSetupRuleTypes[0];
        }

        activeSetupRule = System.Activator.CreateInstance(selectedType) as ISetupRule;

        if (activeSetupRule is BaseRule baseRule)
        {
            baseRule.SetRuleManager(this);
        }

        activeSetupRule.Initialize();
    }

    /// <summary>
    /// Selects and initializes multiple play rules
    /// </summary>
    private void SelectPlayRules()
    {
        if (availablePlayRuleTypes.Count == 0)
        {
            Debug.LogWarning("No play rules available!");
            return;
        }

        int ruleCount = Random.Range(minPlayRules, maxPlayRules + 1);
        ruleCount = Mathf.Min(ruleCount, availablePlayRuleTypes.Count);

        if (ruleCount == 0)
        {
            Debug.Log("No play rules selected");
            return;
        }

        // Shuffle and select random play rules
        var shuffledRules = availablePlayRuleTypes.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < ruleCount; i++)
        {
            IPlayRule newRule = System.Activator.CreateInstance(shuffledRules[i]) as IPlayRule;

            // Check compatibility
            bool compatible = true;

            if (activeSetupRule != null)
            {
                if (!newRule.IsCompatibleWith(activeSetupRule) || !activeSetupRule.IsCompatibleWith(newRule))
                {
                    compatible = false;
                    Debug.Log($"Rule {newRule.RuleName} incompatible with {activeSetupRule.RuleName}");
                }
            }

            if (compatible)
            {
                foreach (var existingRule in activePlayRules)
                {
                    if (!newRule.IsCompatibleWith(existingRule) || !existingRule.IsCompatibleWith(newRule))
                    {
                        compatible = false;
                        break;
                    }
                }
            }

            if (compatible)
            {
                if (newRule is BaseRule baseRule)
                {
                    baseRule.SetRuleManager(this);
                }

                activePlayRules.Add(newRule);
                newRule.Initialize();
            }
        }
    }

    private void LogActiveRules()
    {
        Debug.Log("═══════════════════════════════════");
        Debug.Log("      GENERATED RULE SET");
        Debug.Log("═══════════════════════════════════");
        Debug.Log($"GAME MODE: {activeSetupRule?.RuleName ?? "None"}");
        if (activeSetupRule != null)
        {
            Debug.Log($"  {activeSetupRule.Description}");
        }

        Debug.Log($"\nACTIVE MODIFIERS: {activePlayRules.Count}");
        foreach (var rule in activePlayRules)
        {
            Debug.Log($"  • {rule.RuleName}");
        }

        Debug.Log("═══════════════════════════════════\n");
    }

    /// <summary>
    /// Execute the setup rule
    /// </summary>
    public void ExecuteSetupRule()
    {
        activeSetupRule?.Execute();
    }

    /// <summary>
    /// Execute all active play rules (in priority order)
    /// </summary>
    public void ExecutePlayRules()
    {
        foreach (var rule in activePlayRules)
        {
            rule.Execute();
        }
    }

    /// <summary>
    /// Execute all rules
    /// </summary>
    public void ExecuteAllRules()
    {
        ExecuteSetupRule();
        ExecutePlayRules();
    }

    /// <summary>
    /// Clear all active rules
    /// </summary>
    public void ClearRules()
    {
        activeSetupRule?.Cleanup();
        activeSetupRule = null;

        foreach (var rule in activePlayRules)
        {
            rule.Cleanup();
        }

        activePlayRules.Clear();
    }

    public ISetupRule GetSetupRule() => activeSetupRule;
    public List<IPlayRule> GetPlayRules() => new List<IPlayRule>(activePlayRules);
    public T GetSetupRuleAs<T>() where T : class, ISetupRule => activeSetupRule as T;
    public T GetPlayRule<T>() where T : class, IPlayRule => activePlayRules.OfType<T>().FirstOrDefault();

    void OnDestroy()
    {
        ClearRules();
    }
}
