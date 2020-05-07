using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library_Task
{
    public class WorkerProgress
    {
        CancellationTokenSource _cts;
        IProgress<int> _progress;
        int _max;
        int _delay;

        public WorkerProgress(int max, int delay, CancellationTokenSource cts, IProgress<int> progress)
        {
            _max = max;
            _delay = delay;
            _cts = cts;
            _progress = progress;
        }

        public void Start()
        {
            Task.Factory.StartNew(DoWork);
        }

        private void DoWork()
        {
            for (int i = 0; i < _max; i++)
            {
                NotifyProgress(_progress, i);
                Thread.Sleep(_delay);
                if (_cts.IsCancellationRequested)
                    break;
            }
        }

        private void NotifyProgress(IProgress<int> progress, int i)
        {
            progress.Report(i);
        }
    }
}
