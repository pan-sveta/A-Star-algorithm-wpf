﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Maze_solver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MazeController mazeController;
        public MainWindow()
        {
            InitializeComponent();
            mazeController = new MazeController(mainCanvas);
        }

        private void ButtonSolve_Click(object sender, RoutedEventArgs e)
        {
            mazeController.SolveMaze();
        }

        private void ButtonRestart_Click(object sender, RoutedEventArgs e)
        {
            mazeController.Restart();
        }
    }
}
