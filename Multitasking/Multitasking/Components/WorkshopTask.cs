/******************************************************************/
/// <copyright file="WorkshopTask.cs" company="Sharpro">
/// Copyright (c) 2018 All Rights Reserved
/// </copyright>
/// <author>Jarosław Mielewski</author>
/// <date>19:13</date>
/******************************************************************/

namespace Multitasking.Components
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    class WorkshopTask : ITask, INotifyPropertyChanged
    {
        private readonly TimeSpan TaskForSureStopped = TimeSpan.FromMilliseconds(200);

        private static readonly Object locker = new Object();
        private int progressValue;
        private bool isRunning;
        private CancellationTokenSource cancelSource;

        public WorkshopTask(string taskName)
        {
            cancelSource = new CancellationTokenSource();
            Progress = -1;
            Name = taskName;
        }

        public string Name { get; private set; }

        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
        }

        public int Progress
        {
            get
            {
                lock (locker)
                {
                    return progressValue;
                }
            }
            private set
            {
                lock (locker)
                {
                    progressValue = value;
                    NotifyPropertyChanged(nameof(Progress));
                }
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async Task Start(int startProgress = 0)
        {
            cancelSource = new CancellationTokenSource();
            Progress = startProgress;
            isRunning = true;
            await DoWork();
        }

        public async Task Stop()
        {
            isRunning = false;
            cancelSource.Cancel();
            Thread.Sleep(TaskForSureStopped);
        }

        public void Dispose()
        {
            Stop();
            cancelSource.Dispose();
        }

        private async Task DoWork()
        {
            await Task.Run(() =>
            {
                while (Progress < 100)
                {
                    if (cancelSource.IsCancellationRequested)
                    {
                        break;
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    Progress++;
                }
            }, cancelSource.Token);
        }
    }
}
