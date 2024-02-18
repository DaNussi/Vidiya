using Avalonia.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vidiya.Managers
{
    public abstract class Manager
    {
        public LogManager logger;


        public Manager(LogManager logger = null)
        {
            this.logger = logger;
        }

        public abstract void Init();
        public abstract void Exit();
    }
}
