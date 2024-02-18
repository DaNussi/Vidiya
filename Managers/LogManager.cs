using Avalonia.Controls;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vidiya.Windows;

namespace Vidiya.Managers
{
    public class LogManager : Manager
    {
        private static bool SHOW_LOG_WINDOW_ON_INIT = true;

        List<LogLine> logs = new List<LogLine>();
        List<LogListener> listeners = new List<LogListener>();

        public LogManager() : base()
        {
            this.logger = this;
        }

        public void log(LogType type, string message)
        {
            var methodInfo = new StackTrace().GetFrame(1).GetMethod();
            var className = methodInfo.ReflectedType.Name;

            LogLine line = new LogLine(type, message, DateTime.Now, className);
            logs.Add(line);
            listeners.ForEach(listerner => { listerner.OnLog(line, logs); });
        }


        public void removeListener(LogListener listener)
        {
            listeners.Remove(listener);
        }

        public void addListener(LogListener listener)
        {
            foreach(LogLine line in logs)
            {
                listener.OnLog(line, logs);
            }
            listeners.Add(listener);
        }

        public override void Init()
        {
            if(SHOW_LOG_WINDOW_ON_INIT)
            {
                LogWindow window = new LogWindow();
                window.Show();
            }
        }

        public override void Exit()
        {

        }
    }

    public class LogLine
    {
        public string message {  get; }
        public LogType type { get; }
        public DateTime time { get; }
        public string? className { get; }

        public LogLine(LogType type, string message, DateTime time,string? className)
        {
            this.type = type;
            this.message = message;
            this.time = time;
            this.className = className;
        }
    
        public string getLine()
        {
            string p1 = className != null ? "[" + className + "] " : "";
            while (p1.Length < 16) p1 = p1 + " ";
            p1 += "| ";

            string p2 = type + " | ";
            while (p2.Length < 10) p2 = " " + p2;

            string p3 = message;
            return p1 + p2 + p3;
        }
    }

    public enum LogType
    {
        Info, Warning, Error
    }

    public class LogListener
    {
        Action<LogLine, List<LogLine>> actoion;

        public LogListener(Action<LogLine,List<LogLine>> actoion)
        {
            this.actoion = actoion;
        }
        public LogListener(Action<LogLine, List<LogLine>> actoion, LogManager logManager) 
        {
            this.actoion = actoion;
            logManager.addListener(this);
        }

        public void OnLog(LogLine logLine, List<LogLine> logs) {
            this.actoion.Invoke(logLine, logs);
        }
    }
}
