namespace Rules
{
    /// <summary>
    /// Base interface that all game rules must implement
    /// </summary>
    public interface IRule
    {
        /// <summary>
        /// The name/description of this rule
        /// </summary>
        string RuleName { get; }

        /// <summary>
        /// Initialize the rule when it becomes active
        /// </summary>
        void Initialize();

        /// <summary>
        /// Called when the rule should be applied/checked
        /// </summary>
        void Execute();

        /// <summary>
        /// Clean up when the rule is no longer active
        /// </summary>
        void Cleanup();

        /// <summary>
        /// Optional: Check if this rule can be combined with another rule
        /// </summary>
        bool IsCompatibleWith(IRule otherRule);
    }

    /// <summary>
    /// Setup rules define the base game configuration (win conditions, game modes, etc.)
    /// Only ONE setup rule is active at a time
    /// </summary>
    public interface ISetupRule : IRule
    {
        string[] rulesList { get; }
        /// <summary>
        /// Description of what this game mode is about
        /// </summary>
        string Description { get; }

        void SetupGame();
    }

    /// <summary>
    /// Play rules are modifiers that add extra mechanics to the base game
    /// Multiple play rules can be active simultaneously
    /// </summary>
    public interface IPlayRule : IRule
    {
    }
}