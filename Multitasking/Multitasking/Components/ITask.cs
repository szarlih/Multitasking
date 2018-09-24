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
    using System.Threading.Tasks;

    interface ITask
    {
        string Name { get; }
        Task Start();
        void Stop();
        int Progress { get; }
        bool IsRunning { get; }
    }
}
