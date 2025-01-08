namespace TurnBasedGame.Type
{
    /// <summary>
    /// In Honkai Star Rail i belive they only have 2 State. 
    /// Exploring and Battle. but there's a third one for transition when pre-battle the enemy. 
    /// still not sure how can i implement it.
    /// </summary>
    public enum GameState 
    {
        Exploring = 0,
        Battle = 1,
        Transition = 2,
    }
}