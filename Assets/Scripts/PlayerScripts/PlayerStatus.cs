namespace Powerlifter.PlayerScripts
{
    public class PlayerStatus
    {
        public static int Level { get; private set; } = 0;
        public static int UpdatePrice => (Level + 1) * 20;
        public static int CorpsesPossible => (Level + 1) * 3;
        public static void UpdatePlayerStatus() => Level++;
    }
}
