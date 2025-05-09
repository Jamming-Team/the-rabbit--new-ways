namespace Rabbit
{
    public static class GC
    {
        public static class Scenes
        {
            public const string CORE = "Core";
            public const string EMPTY = "Empty";
            public const string MAIN_MENU = "MainMenu";
            public const string GAMEPLAY = "Gameplay";
        }

        public static class UI {
            public enum ButtonTypes {
                Play,
                Settings,
                Back,
                Pause,
                Quit,
                Restart,
            }
        }
    }
}