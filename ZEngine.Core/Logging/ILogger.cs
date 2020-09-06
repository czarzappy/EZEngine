using System;

namespace ZEngine.Unity.Core.Logging
{
    public interface ILogger
    {
        void Info(string message);
        void Warn(string message);
        void Error(string message);
        void Ex(Exception exception);
    }
}