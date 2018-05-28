using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freefy
{
    class SimpleMutex
    {
        readonly object _lock = new object();
        private bool acquired = false;

        public void Acquire()
        {
            while (true)
                lock (_lock)
                    if (!acquired)
                        return;
        }

        public void Release()
        {
            lock (_lock)
                acquired = false;
        }
    }
}
