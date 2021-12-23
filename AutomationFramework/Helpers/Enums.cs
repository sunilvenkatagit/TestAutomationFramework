namespace AutomationFramework.Libraries
{
    public static class Enums
    {
        public enum WaitStrategy
        {
            CLICKABLE,
            PRESENT,
            VISIBLE
        }

        public enum SelectBy
        {
            INDEX,
            TEXT,
            VALUE
        }

        public enum Window
        {
            CURRENTWINDOW = 0,
            SECONDWINDOW = 1,
            THIRDWINDOW = 2
        }

        public enum Perform
        {
            SINGLECLICK,
            DOUBLECLICK
        }
    }
}
