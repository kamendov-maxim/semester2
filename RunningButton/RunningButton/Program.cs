namespace RunningButton
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!System.OperatingSystem.IsWindows())
            {
                Console.WriteLine("Program is only supported on Windows");
                System.Environment.Exit(0);
            }
            ApplicationConfiguration.Initialize();
            Application.Run(new RunningButtonForm());
        }
    }
}