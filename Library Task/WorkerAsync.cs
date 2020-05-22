using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library_Task
{
    public class WorkerAsync
    {
        CancellationTokenSource _cts;
        int _max;
        int _delay;

        public WorkerAsync(int max, int delay, CancellationTokenSource cts)
        {
            _max = max;
            _delay = delay;
            _cts = cts;
        }

        public async Task Start()
        {
            await Task.Factory.StartNew(DoWork);
        }

        //async Task<T>

        private void DoWork()
        {
            for (int i = 0; i < _max; i++)
            {
                Thread.Sleep(_delay);
                if (_cts.IsCancellationRequested)
                    break;
            }
        }
    }
}
