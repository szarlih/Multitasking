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
        private static readonly Object locker = new Object();
        private int progressValue;
        private bool isRunning;

        public WorkshopTask(string taskName)
        {
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

        public async Task Start()
        {
            Progress = 0;
            isRunning = true;
            await DoWork();
        }

        public void Stop()
        {
            isRunning = false;
        }

        private async Task DoWork()
        {
            await Task.Run(() =>
            {
                while (Progress < 100)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    Progress++;
                }
            });
        }
    }
}
