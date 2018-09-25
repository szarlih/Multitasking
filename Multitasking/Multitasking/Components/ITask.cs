/******************************************************************/
/// <copyright file="ITask.cs" company="Sharpro">
/// Copyright (c) 2018 All Rights Reserved
/// </copyright>
/// <author>Jarosław Mielewski</author>
/// <date>19:02</date>
/******************************************************************/

/// <copyright file="ITask.cs" company="Sharpro">
/// Copyright (c) 2018 All Rights Reserved
/// </copyright>
/// <author>Jarosław Mielewski</author>
/// <date>18:56</date>
namespace Multitasking.Components
{
    using System;
    using System.Threading.Tasks;

    interface ITask : IDisposable
    {
        string Name { get; }
        Task Start(int startProgress = 0);
        Task Stop();
        int Progress { get; }
        bool IsRunning { get; }
    }
}
