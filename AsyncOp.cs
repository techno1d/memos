using System;
using System.Threading;
using System.Threading.Tasks;

namespace UtilClasses
{
    public class AsyncOp<T>
    {
        Task<T> _pendingTask = null;
        CancellationTokenSource _pendingCts = null;

        public Task<T> PendingTask => _pendingTask;

        public void Cancel()
        {
            if (_pendingTask != null && !_pendingTask.IsCompleted)
                _pendingCts.Cancel();
        }

        public Task<T> RunAsync(Func<CancellationToken, Task<T>> routine, CancellationToken token)
        {
            var oldTask = _pendingTask;
            var oldCts = _pendingCts;

            var thisCts = CancellationTokenSource.CreateLinkedTokenSource(token);

            Func<Task<T>> startAsync = async () =>
            {
                // await the old task
                if (oldTask != null && !oldTask.IsCompleted)
                {
                    oldCts.Cancel();
                    try
                    {
                        await oldTask;
                    }
                    catch (System.Exception ex)
                    {
                        while (ex is AggregateException)
                            ex = ex.InnerException;
                        if (!(ex is System.OperationCanceledException))
                            throw;
                    }
                }
                // run and await this task
                return await routine(thisCts.Token);
            };

            _pendingCts = thisCts;

            _pendingTask = Task.Factory.StartNew(
                startAsync,
                _pendingCts.Token,
                TaskCreationOptions.None,
                TaskScheduler.FromCurrentSynchronizationContext()).Unwrap();

            return _pendingTask;
        }
    }
}
