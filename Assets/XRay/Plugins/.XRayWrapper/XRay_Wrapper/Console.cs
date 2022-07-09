namespace XRay
{
    public static class Console
    {
        public const int MAX_SIZE = 1024;
        public static void Log(string text)
        {
            if (MainForm.Instance.ConsoleBox.Items.Count >= MAX_SIZE)
            {
                MainForm.Instance.ConsoleBox.Items.RemoveAt(0);
            }
            MainForm.Instance.ConsoleBox.Items.Add("[Log] " + text);
        }

        public static void Error(string text)
        {
            if (MainForm.Instance.ConsoleBox.Items.Count >= MAX_SIZE)
            {
                MainForm.Instance.ConsoleBox.Items.RemoveAt(0);
            }
            MainForm.Instance.ConsoleBox.Items.Add("[Error] " + text);
        }

        public static void Warning(string text)
        {
            if (MainForm.Instance.ConsoleBox.Items.Count >= MAX_SIZE)
            {
                MainForm.Instance.ConsoleBox.Items.RemoveAt(0);
            }
            MainForm.Instance.ConsoleBox.Items.Add("[Warning] " + text);
        }
    }
}
