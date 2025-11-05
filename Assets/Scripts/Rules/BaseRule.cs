using UnityEngine;

namespace Rules
{
    /// <summary>
    /// Base implementation of IRule with common functionality
    /// </summary>
    public abstract class BaseRule : IRule
    {
        public abstract string RuleName { get; }

        protected RuleManager ruleManager;

        public virtual void Initialize()
        {
            Debug.Log($"Rule initialized: {RuleName}");
        }

        public abstract void Execute();

        public virtual void Cleanup()
        {
            Debug.Log($"Rule cleaned up: {RuleName}");
        }

        public virtual bool IsCompatibleWith(IRule otherRule)
        {
            // By default, all rules are compatible
            // Override this in specific rules if needed
            return true;
        }

        public void SetRuleManager(RuleManager manager)
        {
            ruleManager = manager;
        }
    }

    /// <summary>
    /// Base class for setup rules that define the base game
    /// </summary>
    public abstract class BaseSetupRule : BaseRule, ISetupRule
    {
        public abstract string[] rulesList { get; }
        
        public abstract string Description { get; }

        public abstract void SetupGame();

        public override void Initialize()
        {
            base.Initialize();
            SetupGame();
        }

        public override bool IsCompatibleWith(IRule otherRule)
        {
            // Setup rules are never compatible with other setup rules
            if (otherRule is ISetupRule)
                return false;

            return true;
        }
    }

    /// <summary>
    /// Base class for play rules that modify gameplay
    /// </summary>
    public abstract class BasePlayRule : BaseRule, IPlayRule
    {
    }
}