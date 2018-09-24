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
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<ITask> tasks;

        public MainWindow()
        {
            InitializeComponent();
            tasks = new ObservableCollection<ITask>();
            TaskListBox.ItemsSource = tasks;
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if(IsUnique())
            {
                var task = new WorkshopTask(TaskNameTextBox.Text);
                tasks.Add(task);
                await task.Start();
            }
            else
            {
                MessageBox.Show("Task exists");
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {

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
