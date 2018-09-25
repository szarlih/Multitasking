/******************************************************************/
/// <copyright file="MainWindow.xaml.cs" company="Sharpro">
/// Copyright (c) 2018 All Rights Reserved
/// </copyright>
/// <author>Jarosław Mielewski</author>
/// <date>18:53</date>
/******************************************************************/

namespace Multitasking
{
    using Multitasking.Components;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        private ObservableCollection<ITask> tasks;

        public MainWindow()
        {
            InitializeComponent();
            tasks = new ObservableCollection<ITask>();
            TaskListBox.ItemsSource = tasks;
        }

        public void Dispose()
        {
            foreach(var task in tasks)
            {
                task.Dispose();
            }

            tasks.Clear();
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if(IsUnique())
            {
                var task = new WorkshopTask(TaskNameTextBox.Text);
                tasks.Add(task);
                await task.Start();
                TaskNameTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Task exists");
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if(TaskListBox.SelectedIndex < 0)
            {
                return;
            }

            var task = TaskListBox.SelectedItem as ITask;

            if (task.IsRunning)
            {
                task.Stop();
            }
        }

        private async void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListBox.SelectedIndex < 0)
            {
                return;
            }

            var task = TaskListBox.SelectedItem as ITask;

            if (task.IsRunning)
            {
                await task.Stop();
            }

            task.Start();
        }


        private void Resume_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListBox.SelectedIndex < 0)
            {
                return;
            }

            var task = TaskListBox.SelectedItem as ITask;

            if (!task.IsRunning)
            {
                task.Start(task.Progress);
            }
        }

        private bool IsUnique()
        {
            if(!tasks.Any())
            {
                return true;
            }

            foreach(var task in tasks)
            {
                if(task.Name.Equals(TaskNameTextBox.Text))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
