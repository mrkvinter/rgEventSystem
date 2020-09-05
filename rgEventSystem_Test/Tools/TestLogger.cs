using System;

namespace rgEventSystem_Test.Tools
{
    public class TestLogger : rgEventSystem.Log.ILogger
    {
        public void Info(string message)
        {
            Console.WriteLine(message);
        }

        public void Warning(string message)
        {
            Console.WriteLine(message);
        }

        public void Error(string message)
        {
            Console.WriteLine(message);
        }

        public void Error(Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
}