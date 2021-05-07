using System;
using System.Threading;
using System.Threading.Tasks;
using Cronos;
using Microsoft.Extensions.Hosting;

namespace VaccinationInfoProvider.VaccinationInfoFetcherService {
    public class CronJobService: IHostedService, IDisposable {

        private System.Timers.Timer timer;
        private readonly CronExpression _expression;
        private readonly TimeZoneInfo _timeZoneInfo;


        public CronJobService() {
            _expression = CronExpression.Parse("*/5****"); // Runs every 5 mins.
            _timeZoneInfo = TimeZoneInfo.Local;
        }

        /// <inheritdoc />
        public async Task StartAsync(CancellationToken cancellationToken) {
            await ScheduleJob(cancellationToken);
        }

        /// <inheritdoc />
        public async Task StopAsync(CancellationToken cancellationToken) {
            timer?.Stop();
            await Task.CompletedTask;
        }


        protected virtual async Task ScheduleJob(CancellationToken cancellationToken) {
            var next = _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);

            if (next.HasValue) {
                var delay = next.Value - DateTimeOffset.Now;
                // prevent non-positive values from being passed into Timer
                if (delay.TotalMilliseconds <= 0) {
                    await ScheduleJob(cancellationToken);
                }

                timer = new System.Timers.Timer(delay.TotalMilliseconds);
                timer.Elapsed += async (sender, args) => {
                    timer.Dispose();  // reset and dispose timer
                    timer = null;

                    if (!cancellationToken.IsCancellationRequested) {
                        await DoWork(cancellationToken);
                    }

                    if (!cancellationToken.IsCancellationRequested) {
                        await ScheduleJob(cancellationToken);    // reschedule next
                    }
                };
                timer.Start();
            }
            await Task.CompletedTask;
        }

        public virtual async Task DoWork(CancellationToken cancellationToken) {
            await Task.Delay(5000, cancellationToken);  // do the work
        }


        protected virtual void Dispose(bool disposing) {
            timer.Dispose();
        }

        /// <inheritdoc />
        public void Dispose() {
            Dispose(true);
        }

        /// <summary>
        /// Finalizer
        /// </summary>
        ~CronJobService() {
            Dispose(false);
        }
    }
}