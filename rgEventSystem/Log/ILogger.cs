using System;

namespace rgEventSystem.Log
{
    public interface ILogger
    {
        void Info(string message);

        void Warning(string message);

        void Error(string message);

        void Error(Exception exception);
    }
}