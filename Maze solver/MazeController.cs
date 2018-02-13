using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Maze_solver
{
    class MazeController
    {
        private Canvas canvas;
        public Maze Maze { get; private set; }

        public MazeController(Canvas canvas)
        {
            if (ValidateCanvas(canvas))
            {
                this.canvas = canvas;
                Maze = new Maze((int)canvas.Width/25,(int)canvas.Height/25);
            }
            else
            {
                throw new Exception("Invalid canvas size. Dimensions must be devisible by 25.");
            }

            foreach (Field field in Maze.Fields)
            {
                canvas.Children.Add(field.Rectangle);
            }
        }

        private bool ValidateCanvas(Canvas canvas)
        {
            bool validity = false;

            if (canvas.Width % 25 == 0 && canvas.Height % 25 == 0)
            {
                validity = true;
            }

            return validity;
        }

        public void SolveMaze()
        {
            Maze.Solve();
        }
    }
}
